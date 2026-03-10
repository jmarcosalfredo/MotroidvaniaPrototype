using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_DomainExpansion : Skill_Base
{
    [SerializeField] private GameObject domainPrefab;

    [Header("Slow Down Upgrade")]
    [SerializeField] private float spellsToCast = 10;
    [SerializeField] private float slowDownPercent = 0.8f;
    [SerializeField] private float slowDownDomainDuration = 5f;

    [Header("Spell Casting Upgrade")]
    [SerializeField] private float spellCastDomainSlowDownPercent = 1f;
    [SerializeField] private float spellCastDomainDuration = 8f;
    private float spellCastTimer;
    private float spellsPerSecound;

    [Header("Domain details")]
    [SerializeField] private float maxDomainSize = 10f;
    [SerializeField] private float expandSpeed = 3f;


    private List<Enemy> trappedTargets = new List<Enemy>();
    private Transform currentTarget;

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

    public void DoSpellCasting()
    {
        spellCastTimer -= Time.deltaTime;

        if (currentTarget == null)
        {
            currentTarget = FindTargetInDomain();
        }

        if (currentTarget != null && spellCastTimer < 0)
        {
            CastSpell(currentTarget);
            spellCastTimer = 1f / spellsPerSecound;
            currentTarget = null;
        }
    }

    private void CastSpell(Transform target)
    {
        if ( upgradeType == SkillUpgradeType.Domain_EchoSpam)
        {
            Vector3 offset = Random.value < 0.5f ? new Vector2 (1,0) : new Vector2(-1,0);

            skillManager.timeEcho.CreateTimeEcho(target.position + offset);
        }

        if (upgradeType == SkillUpgradeType.Domain_ShardSpam)
        {
            skillManager.shard.CreateRawShard(target, true);
        }
    }

    private Transform FindTargetInDomain()
    {
        if (trappedTargets.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, trappedTargets.Count);
        Transform target = trappedTargets[randomIndex].transform;

        if (target == null)
        {
            trappedTargets.RemoveAt(randomIndex);
            return null;
        }

        return target;
    }

    public bool InstantDomain()
    {
        return upgradeType != SkillUpgradeType.Domain_EchoSpam
            && upgradeType != SkillUpgradeType.Domain_ShardSpam;
    }

    public void CreateDomain()
    {
        spellsPerSecound = spellsToCast / GetDomainDuration();

        GameObject domain = Instantiate(domainPrefab, transform.position, Quaternion.identity);
        domain.GetComponent<SkillObject_DomainExpansion>().SetupDomain(this);
    }

    public void AddTarget(Enemy enemy)
    {
        trappedTargets.Add(enemy);
    }

    public void ClearTargets()
    {
        foreach (var enemy in trappedTargets)
        {
            enemy.StopSlowDown();
        }

        trappedTargets = new List<Enemy>();
    }
}
