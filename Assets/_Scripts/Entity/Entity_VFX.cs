using System.Collections;
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

    private void Awake()
    {
        entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        originaltMaterial = sr.material;
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
