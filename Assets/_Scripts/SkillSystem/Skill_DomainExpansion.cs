using Unity.VisualScripting;
using UnityEngine;

public class Skill_DomainExpansion : Skill_Base
{
    [SerializeField] private GameObject domainPrefab;

    [Header("Slow Down Upgrade")]
    [SerializeField] private float slowDownPercent = 0.8f;
    [SerializeField] private float slowDownDomainDuration = 5f;

    [Header("Spell Casting Upgrade")]
    [SerializeField] private float spellCastDomainSlowDownPercent = 1f;
    [SerializeField] private float spellCastDomainDuration = 8f;


    [Header("Domain details")]
    private float maxDomainSize = 10f;
    private float expandSpeed = 3f;

    public float GetMaxDomainSize() => maxDomainSize;

    public float GetExpandSpeed() => expandSpeed;

    public float GetDomainDuration()
    {
        if (upgradeType == SkillUpgradeType.Domain_SlowDown)
        {
            return slowDownDomainDuration;
        }
        else
        {
            return spellCastDomainDuration;
        }
    }

    public float GetSlowDownPercentage()
    {
        if (upgradeType == SkillUpgradeType.Domain_SlowDown)
        {
            return slowDownPercent;
        }
        else
        {
            return spellCastDomainSlowDownPercent;
        }
    }

    public bool InstantDomain()
    {
        return upgradeType != SkillUpgradeType.Domain_EchoSpam
            && upgradeType != SkillUpgradeType.Domain_ShardSpam;
    }

    public void CreateDomain()
    {
        GameObject domain = Instantiate(domainPrefab, transform.position, Quaternion.identity);
        domain.GetComponent<SkillObject_DomainExpansion>().SetupDomain(this);
    }
}
