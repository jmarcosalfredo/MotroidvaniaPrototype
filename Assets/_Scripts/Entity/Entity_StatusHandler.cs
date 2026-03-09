using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVFX;
    private Entity_Stats entityStats;
    private Entity_Health entityHealth;
    private ElementType currentEffect = ElementType.None;

    [Header("Shock effect details")]
    [SerializeField] private GameObject lightningStrikeVfxPrefab;
    [SerializeField] private float currentCharge;
    [SerializeField] private float maxCharge = 1;
    private Coroutine shockCo;

    private void Awake()
    {
        entityStats = GetComponent<Entity_Stats>();
        entityHealth = GetComponent<Entity_Health>();
        entity = GetComponent<Entity>();
        entityVFX = GetComponent<Entity_VFX>();
    }

    public void RemoveAllNegativeEffects()
    {
        StopAllCoroutines();
        currentEffect = ElementType.None;
        entityVFX.StopAllVfx();
    }

    public void ApplyStatusEffect(ElementType element, ElementalEffectData effectData)
    {
        if (element == ElementType.Ice && CanBeApplied(ElementType.Ice))
        {
            ApplyChillEffect(effectData.chillDuration, effectData.chillSlowEffect);
        }
        if (element == ElementType.Fire && CanBeApplied(ElementType.Fire))
        {
            ApplyBurnEffect(effectData.burnDuration, effectData.burnDamage);
        }
        if (element == ElementType.Lightning && CanBeApplied(ElementType.Lightning))
        {
            ApplyShockEffect(effectData.shockDuration, effectData.shockDamage, effectData.shockCharge);
        }
    }

    private void ApplyShockEffect(float duration, float damage, float charge)
    {
        float lightningResistance = entityStats.GetElementalResistance(ElementType.Lightning);
        float finalCharge = charge * (1f - lightningResistance);
        currentCharge += finalCharge;

        if (currentCharge >= maxCharge)
        {
            DoLightningStrike(damage);
            StopShockEffect();
            return;
        }

        if(shockCo != null)
        {
            StopCoroutine(shockCo);
        }

        shockCo = StartCoroutine(ShockEffectCo(duration));
    }

    private void StopShockEffect()
    {
        currentEffect = ElementType.None;
        currentCharge = 0;
        entityVFX.StopAllVfx();
    }

    private void DoLightningStrike(float damage)
    {
        Instantiate(lightningStrikeVfxPrefab, transform.position, Quaternion.identity);
        entityHealth.ReduceHealth(damage);
    }

    private IEnumerator ShockEffectCo(float duration)
    {
        currentEffect = ElementType.Lightning;
        entityVFX.PlayOnStatusVfx(duration, ElementType.Lightning);

        yield return new WaitForSeconds(duration);
        StopShockEffect();
    }

    private void ApplyBurnEffect(float duration, float fireDamage)
    {
        float fireResistance = entityStats.GetElementalResistance(ElementType.Fire);
        float finalDamage = fireDamage * (1f - fireResistance);
        StartCoroutine(BurnEffectCo(duration, finalDamage));
    }

    private IEnumerator BurnEffectCo(float duration, float totalDamage)
    {
        currentEffect = ElementType.Fire;
        entityVFX.PlayOnStatusVfx(duration, ElementType.Fire);

        int ticksPerSecond = 2;
        int tickCount = Mathf.RoundToInt(ticksPerSecond * duration);

        float damagePerTick = totalDamage / tickCount;
        float tickInterval = 1f / ticksPerSecond;

        for(int i = 0; i < tickCount; i++)
        {
            entityHealth.ReduceHealth(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        currentEffect = ElementType.None;
    }

    private void ApplyChillEffect(float duration , float slowMultiplier)
    {
        float iceResistance = entityStats.GetElementalResistance(ElementType.Ice);
        float finalDuration = duration * ( 1 - iceResistance);

        StartCoroutine(ChillEffectCo(finalDuration, slowMultiplier));
    }

    private IEnumerator ChillEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementType.Ice;
        entityVFX.PlayOnStatusVfx(duration, ElementType.Ice);

        yield return new WaitForSeconds(duration);
        currentEffect = ElementType.None;
    }

    public bool CanBeApplied(ElementType element)
    {
        if (element == ElementType.Lightning && currentEffect == ElementType.Lightning)
            return true;

        return currentEffect == ElementType.None;
    }
}
