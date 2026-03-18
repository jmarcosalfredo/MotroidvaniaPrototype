using System;
using UnityEngine;

[Serializable]
public class ElementalEffectData
{
    public float chillDuration;
    public float chillSlowEffect;

    public float burnDuration;
    public float burnDamage;

    public float shockDuration;
    public float shockDamage;
    public float shockCharge;

    public ElementalEffectData(Entity_Stats entityStats, ScaleEffectData damageScale)
    {
        chillDuration = damageScale.chillDuration;
        chillSlowEffect = damageScale.chillSlowMultiplier;

        burnDuration = damageScale.burnDuration;
        burnDamage = entityStats.offense.fireDamage.GetValue() * damageScale.burnDamageScale;

        shockDuration = damageScale.shockDuration;
        shockDamage = entityStats.offense.lightningDamage.GetValue() * damageScale.shockDamageScale;
        shockCharge = damageScale.shockCharge;
    }

}
