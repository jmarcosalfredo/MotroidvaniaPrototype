
using System;
using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat_SetupSO defaultStatSetup;

    public Stat_ResourceGroup resource;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;
    public Stat_MajorGroup major;

    public AttackData GetAttackData(ScaleEffectData scaleData)
    {
        return new AttackData(this, scaleData);
    }

    public float GetElementalDamage(out ElementType element, float  scaleFactor = 1f)
    {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float lightningDamage = offense.lightningDamage.GetValue();
        float bonusElementalDamage = major.intelligence.GetValue();

        float highestDamage = fireDamage;
        element = ElementType.Fire;

        if(iceDamage > highestDamage)
        {
            highestDamage = iceDamage;
            element = ElementType.Ice;
        }

        if(lightningDamage > highestDamage)
        {
            highestDamage = lightningDamage;
            element = ElementType.Lightning;
        }

        if (highestDamage <= 0)
        {
            element = ElementType.None;
            return 0;
        }

        float bonusFire = (element == ElementType.Fire) ? 0 : fireDamage * 0.5f;
        float bonusIce = (element == ElementType.Ice) ? 0 : iceDamage * 0.5f;
        float bonusLightning = (element == ElementType.Lightning) ? 0 : lightningDamage * 0.5f;

        float weakerElementsDamage = bonusFire + bonusIce + bonusLightning;
        float finalDamage = highestDamage + weakerElementsDamage + bonusElementalDamage;

        return finalDamage * scaleFactor;

    }

    public float GetElementalResistance(ElementType element)
    {
        float baseResistance = 0;
        float bonusResistance = major.intelligence.GetValue() * 0.5f;

        switch (element)
        {
            case ElementType.Fire:
                baseResistance = defense.fireRes.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defense.iceRes.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defense.lightningRes.GetValue();
                break;
        }

        float resistance = baseResistance + bonusResistance;
        float resistanceCap = 75f;
        float finalResistance = Mathf.Clamp(resistance, 0f, resistanceCap) / 100f;

        return finalResistance;
    }

    public float GetPhysicalDamage(out bool isCrit, float scaleFactor = 1f)
    {
        float baseDmg = GetBaseDamage();
        float critChance = GetCritChance();
        float critPower = GetCritPower();

        isCrit = UnityEngine.Random.Range(0f, 100f) < critChance;
        float finalDmg = isCrit ? baseDmg * critPower : baseDmg;

        return finalDmg * scaleFactor;
    }

    public float GetBaseDamage() => offense.damage.GetValue() + major.strength.GetValue();
    public float GetCritChance() => offense.critChance.GetValue() + (major.agility.GetValue() * 0.3f);
    public float GetCritPower() => offense.critPower.GetValue() + (major.strength.GetValue() * 0.5f);

    public float GetArmorMitigation(float armorReduction)
    {
        float totalArmor = GetBaseArmor();

        float reductionMultiplier = Math.Clamp(1f - armorReduction, 0f, 1f);
        float effectiveArmor = totalArmor * reductionMultiplier;

        float mitigation = effectiveArmor / (effectiveArmor + 100f); // Using a common armor mitigation formula ( like Diablo and WoW )
        float mitigationCap = 0.85f; // Cap mitigation at 85%

        float finalMitigation = Mathf.Clamp(mitigation, 0f, mitigationCap);
        return finalMitigation;
    }

    public float GetBaseArmor() => defense.armor.GetValue() + major.vitality.GetValue();

    public float GetArmorReduction()
    {
        float finalReduction = offense.armorReduction.GetValue() / 100f;

        return finalReduction;
    }

    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * 0.5f;

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85f;
        float finalEvasion = Mathf.Clamp(totalEvasion, 0f, evasionCap);

        return finalEvasion;
    }

    public float GetMaxHealth()
    {
        float baseMaxHealth = resource.maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public Stat GetStatByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth:
                return resource.maxHealth;
            case StatType.HealthRegen:
                return resource.healthRegen;

            case StatType.Strength:
                return major.strength;
            case StatType.Agility:
                return major.agility;
            case StatType.Inteligence:
                return major.intelligence;
            case StatType.Vitality:
                return major.vitality;

            case StatType.AttackSpeed:
                return offense.attackSpeed;
            case StatType.Damage:
                return offense.damage;
            case StatType.CritChance:
                return offense.critChance;
            case StatType.CritPower:
                return offense.critPower;
            case StatType.ArmorReduction:
                return offense.armorReduction;
            case StatType.FireDamage:
                return offense.fireDamage;
            case StatType.IceDamage:
                return offense.iceDamage;
            case StatType.LightningDamage:
                return offense.lightningDamage;

            case StatType.Armor:
                return defense.armor;
            case StatType.Evasion:
                return defense.evasion;
            case StatType.IceResistance:
                return defense.iceRes;
            case StatType.FireResistance:
                return defense.fireRes;
            case StatType.LightningResistance:
                return defense.lightningRes;
                
            default:
                Debug.LogWarning($"StatType {type} not found!");
                return null;
        }
    }

    [ContextMenu("Update Default Setup")]
    public void ApplyDefaultStatSetup()
    {
        if (defaultStatSetup == null)
        {
            Debug.LogError("Default Stat Setup is not assigned!");
            return;
        }

        resource.maxHealth.SetBaseValue(defaultStatSetup.maxHealth);
        resource.healthRegen.SetBaseValue(defaultStatSetup.healthRegen);

        major.strength.SetBaseValue(defaultStatSetup.strength);
        major.agility.SetBaseValue(defaultStatSetup.agility);
        major.intelligence.SetBaseValue(defaultStatSetup.intelligence);
        major.vitality.SetBaseValue(defaultStatSetup.vitality);

        offense.attackSpeed.SetBaseValue(defaultStatSetup.attackSpeed);
        offense.damage.SetBaseValue(defaultStatSetup.damage);
        offense.critChance.SetBaseValue(defaultStatSetup.critChance);
        offense.critPower.SetBaseValue(defaultStatSetup.critPower);
        offense.armorReduction.SetBaseValue(defaultStatSetup.armorReduction);

        offense.fireDamage.SetBaseValue(defaultStatSetup.fireDamage);
        offense.iceDamage.SetBaseValue(defaultStatSetup.iceDamage);
        offense.lightningDamage.SetBaseValue(defaultStatSetup.lightningDamage);

        defense.armor.SetBaseValue(defaultStatSetup.armor);
        defense.evasion.SetBaseValue(defaultStatSetup.evasion);

        defense.fireRes.SetBaseValue(defaultStatSetup.fireResistance);
        defense.iceRes.SetBaseValue(defaultStatSetup.iceResistance);
        defense.lightningRes.SetBaseValue(defaultStatSetup.lightningResistance);
    }
}
