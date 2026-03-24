using Unity.VisualScripting     ;
using UnityEngine;

public class Skill_TimeEcho : Skill_Base
{
    [SerializeField] private GameObject timeEchoPrefab;
    [SerializeField] private float timeEchoDuration = 5f;

    [Header("Attack Upgrades")]
    [SerializeField] private int maxAttacks = 3;
    [SerializeField] private float duplicateChance = 0.3f;

    [Header("Wisp Upgrades")]
    [SerializeField] private float damagePercentHealed = 0.3f;
    [SerializeField] private float cooldownReducedInSecounds;

    public float GetPercentOfDamageHealed()
    {
        if (ShouldBeWisp() == false)
        {
            return 0;
        }

        return damagePercentHealed;
    }

    public float GetCooldownReducedInSecounds()
    {
        if (upgradeType != SkillUpgradeType.TimeEcho_CooldownWisp)
        {
            return 0;
        }

        return cooldownReducedInSecounds;
    }

    public bool CanRemoveNegativeEffects()
    {
        return upgradeType == SkillUpgradeType.TimeEcho_CleanseWhisp;
    }

    public bool ShouldBeWisp()
    {
        return upgradeType == SkillUpgradeType.TimeEcho_HealWisp
            || upgradeType == SkillUpgradeType.TimeEcho_CleanseWhisp
            || upgradeType == SkillUpgradeType.TimeEcho_CooldownWisp;
    }

    public float GetEchoDuration() => timeEchoDuration;

    public float GetDuplicateChance()
    {
        if (upgradeType != SkillUpgradeType.TimeEcho_ChanceToDuplicate)
        {
            return 0;
        }

        return duplicateChance;
    }

    public int GetMaxAttacks()
    {
        if (upgradeType == SkillUpgradeType.TimeEcho_SinlgleAttack || upgradeType == SkillUpgradeType.TimeEcho_ChanceToDuplicate)
        {
            return 1;
        }

        if (upgradeType == SkillUpgradeType.TimeEcho_MultiAttack)
        {
            return maxAttacks;
        }

        return 0;
    }

    public override void TryUseSkill()
    {
        if (CanUseSkill() == false)
        {
            return;
        }

        CreateTimeEcho();
        SetSkillOnCooldown();
    }

    public void CreateTimeEcho(Vector3? targetPosition = null)
    {
        Vector3 spawnPosition = targetPosition ?? transform.position;

        GameObject timeEcho = Instantiate(timeEchoPrefab, spawnPosition, Quaternion.identity);
        timeEcho.GetComponent<SkillObject_TimeEcho>().SetupEcho(this);
    }
}
