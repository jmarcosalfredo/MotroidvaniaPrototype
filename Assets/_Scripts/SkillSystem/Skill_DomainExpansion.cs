using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_DomainExpansion : Skill_Base
{
    [SerializeField] private GameObject domainPrefab;

    [Header("Slow Down Upgrade")]
    [SerializeField] private float slowDownPercent = 0.8f;
    [SerializeField] private float slowDownDomainDuration = 5f;

    [Header("Shard Casting Upgrade")]
    [SerializeField] private int shardsToCast = 10;
    [SerializeField] private float shardCastDomainSlow = 1f;
    [SerializeField] private float shardCastDomainDuration = 8f;
    private float spellCastTimer;
    private float spellsPerSecound;

    [Header("Echo Casting Upgrade")]
    [SerializeField] private int echoToCast = 8;
    [SerializeField] private float echoCastDomainSlow = 1f;
    [SerializeField] private float echoCastDomainDuration = 6f;
    [SerializeField] private float healthToRestoreWithEcho = 0.05f;

    [Header("Domain details")]
    [SerializeField] private float maxDomainSize = 10f;
    [SerializeField] private float expandSpeed = 3f;


    private List<Enemy> trappedTargets = new List<Enemy>();
    private Transform currentTarget;

    public float GetMaxDomainSize() => maxDomainSize;

    public float GetExpandSpeed() => expandSpeed;

    public float GetDomainDuration()
    {
        switch (upgradeType)
        {
            case SkillUpgradeType.Domain_SlowDown:
                return slowDownDomainDuration;

            case SkillUpgradeType.Domain_EchoSpam:
                return echoCastDomainDuration;

            case SkillUpgradeType.Domain_ShardSpam:
                return shardCastDomainDuration;

            default:
                return 0;
        }
    }

    public float GetSlowDownPercentage()
    {
        switch (upgradeType)
        {
            case SkillUpgradeType.Domain_SlowDown:
                return slowDownPercent;

            case SkillUpgradeType.Domain_EchoSpam:
                return echoCastDomainSlow;

            case SkillUpgradeType.Domain_ShardSpam:
                return shardCastDomainSlow;

            default:
                return 0;
        }
    }

    public int GetSpellsToCast()
    {
        switch (upgradeType)
        {
            case SkillUpgradeType.Domain_EchoSpam:
                return echoToCast;

            case SkillUpgradeType.Domain_ShardSpam:
                return shardsToCast;

            default:
                return 0;
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
        if (upgradeType == SkillUpgradeType.Domain_EchoSpam)
        {
            Vector3 offset = Random.value < 0.5f ? new Vector2(1, 0) : new Vector2(-1, 0);

            skillManager.timeEcho.CreateTimeEcho(target.position + offset);
        }

        if (upgradeType == SkillUpgradeType.Domain_ShardSpam)
        {
            skillManager.shard.CreateRawShard(target, true);
        }
    }

    private Transform FindTargetInDomain()
    {
        trappedTargets.RemoveAll(target => target == null || target.health.isDead);

        if (trappedTargets.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, trappedTargets.Count);
        return trappedTargets[randomIndex].transform;
    }

    public bool InstantDomain()
    {
        return upgradeType != SkillUpgradeType.Domain_EchoSpam
            && upgradeType != SkillUpgradeType.Domain_ShardSpam;
    }

    public void CreateDomain()
    {
        spellsPerSecound = GetSpellsToCast() / GetDomainDuration();

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
