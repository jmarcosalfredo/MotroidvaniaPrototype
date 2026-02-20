using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity entity;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = 0.15f;
    private Material originaltMaterial;
    private Coroutine onDamageVfxCoroutine;

    [Header("On Doing Damage VFX")]
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private GameObject hitVfxPrefab;
    [SerializeField] private GameObject critHitVfxPrefab;

    [Header("Element Colors")]
    [SerializeField] private Color chillVfx = Color.cyan;
    [SerializeField] private Color burnVfx = Color.red;
    [SerializeField] private Color electrifyVfx = Color.yellow;
    private Color originalHitVfxColor;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        originaltMaterial = sr.material;
        originalHitVfxColor = hitVfxColor;
    }

    public void PlayOnStatusVfx(float duration, ElementType element)
    {
        switch (element)
        {
            case ElementType.Ice:
                StartCoroutine(PlayStatusVfxCo(duration, chillVfx));
                break;
            
            case ElementType.Fire:
                StartCoroutine(PlayStatusVfxCo(duration, burnVfx));
                break;

            case ElementType.Lightning:
                StartCoroutine(PlayStatusVfxCo(duration, electrifyVfx));
                break;

            default:
                break;
        }
    }

    public void StopAllVfx()
    {
        StopAllCoroutines();
        sr.color = Color.white;
        sr.material = originaltMaterial;
    }

        private IEnumerator PlayStatusVfxCo(float duration, Color effectColor)
    {
        float thickInterval = 0.2f;
        float timeHasPassed = 0;

        Color lightColor = effectColor * 1.2f;
        Color darkColor = effectColor * 0.8f;

        bool toggle = false;

        while (timeHasPassed < duration)
        {
            sr.color = toggle ? lightColor : darkColor;
            toggle = !toggle;

            yield return new WaitForSeconds(thickInterval);
            timeHasPassed += thickInterval;
        }

        sr.color = Color.white;
    }

    public void CreateOnHitVFX(Transform target, bool isCrit)
    {
        GameObject hitPrefab = isCrit ? critHitVfxPrefab : hitVfxPrefab;
        GameObject vfx = Instantiate(hitPrefab, target.position, Quaternion.identity);

        if(isCrit == false)
        {
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;    
        }

        if(entity.facingDir == -1 && isCrit)
        {
            vfx.transform.Rotate(0f, 180f, 0f);
        }
    }

    public void UpdateOnHitColor(ElementType element)
    {
        switch (element)
        {
            case ElementType.Ice:
                hitVfxColor = chillVfx;
                break;

            default:
                hitVfxColor = originalHitVfxColor;
                break;
        }
    }

    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCoroutine != null)
        {
            StopCoroutine(onDamageVfxCoroutine);
        }

        onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCo());
    }

    private IEnumerator OnDamageVfxCo()
    {
        sr.material = onDamageMaterial;

        yield return new WaitForSeconds(onDamageVfxDuration);

        sr.material = originaltMaterial;
    }
}
