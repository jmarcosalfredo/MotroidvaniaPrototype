using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Material originaltMaterial;
    private Coroutine onDamageVfxCoroutine;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = 0.15f;

    [Header("On Doing Damage VFX")]
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private GameObject hitVfxPrefab;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originaltMaterial = sr.material;
    }

    public void CreateOnHitVFX(Transform target)
    {
       GameObject vfx = Instantiate(hitVfxPrefab, target.position, Quaternion.identity);
       vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;
    }

    public void PlayOnDamageVfx()
    {
        if(onDamageVfxCoroutine != null)
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
