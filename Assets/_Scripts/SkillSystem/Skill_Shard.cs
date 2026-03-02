using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_Shard : Skill_Base
{
    private SkillObject_Shard currentShard;
    private Entity_Health playerHealth;

    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonateTimer = 2f;

    [UnitHeaderInspectable("Moving Shard Upgrade")]
    [SerializeField] private float shardSpeed = 7f;

    [Header("Multicast Upgrade")]
    [SerializeField] private int maxCharges = 3;
    [SerializeField] private int currentCharges;
    [SerializeField] private bool isCharging;

    [Header("Teleport Upgrade")]
    [SerializeField] private float shardExistDuration = 10f;

    [Header("Shard Teleport HP Rewind Upgrade")]
    [SerializeField] private float savedHealthPercent;

    protected override void Awake()
    {
        base.Awake();
        currentCharges = maxCharges;
        playerHealth = GetComponentInParent<Entity_Health>();
    }

    public override void TryUseSkill()
    {
        if (CanUseSkill() == false)
        {
            return;
        }

        switch (upgradeType)
        {
            case SkillUpgradeType.Shard:
                RegularShard();
                break;
            case SkillUpgradeType.Shard_MoveToEnemy:
                HandleShardMoving();
                break;
            case SkillUpgradeType.Shard_MultiCast:
                HandleMulticast();
                break;
            case SkillUpgradeType.Shard_Teleport:
                 HandleShardTeleport();
                break;
            case SkillUpgradeType.Shard_TeleportHpRewind:
                 HandleTeleportRewind();
                break;
        }
    }

    private void HandleTeleportRewind()
    {
        if (currentShard == null)
        {
            CreateShard();
            savedHealthPercent = playerHealth.GetHealthPercent();
        }
        else
        {
            SwapPlayerAndShard();
            playerHealth.SetHealthToPercent(savedHealthPercent);
            SetSkillOnCooldown();
        }
    }

    private void HandleShardTeleport()
    {
        if (currentShard == null)
        {
            CreateShard();
        }
        else
        {
            SwapPlayerAndShard();
            SetSkillOnCooldown();
        }
    }

    private void SwapPlayerAndShard()
    {
        Vector3 shardPosition = currentShard.transform.position;
        Vector3 playerPosition = player.transform.position;

        currentShard.transform.position = playerPosition;
        currentShard.Explode();
        player.TeleportPlayer(shardPosition);
    }

    private void HandleMulticast()
    {
        if (currentCharges <= 0)
        {
            return;
        }

        CreateShard();
        currentShard.MoveTowardsClosestTarget(shardSpeed);
        currentCharges--;

        if (isCharging == false)
        {
            StartCoroutine(ShardRechargeCo());
        }
    }

    private IEnumerator ShardRechargeCo()
    {
        isCharging = true;
        while (currentCharges < maxCharges)
        {
            yield return new WaitForSeconds(cooldown);
            currentCharges++;
        }
        isCharging = false;
    }

    private void HandleShardMoving()
    {
        CreateShard();
        currentShard.MoveTowardsClosestTarget(shardSpeed);
        SetSkillOnCooldown();
    }

    private void RegularShard()
    {
        CreateShard();
        SetSkillOnCooldown();
    }

    public void CreateShard()
    {
        float detonateTimer = GetDetonateTime();

        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        currentShard = shard.GetComponent<SkillObject_Shard>();
        shard.GetComponent<SkillObject_Shard>();
        currentShard.SetupShard(this);

        if (Unlocked(SkillUpgradeType.Shard_Teleport) || Unlocked(SkillUpgradeType.Shard_TeleportHpRewind))
        {
            currentShard.OnExplode += ForceCooldown;
        }
    }

    public void CreateRawShard()
    {
        bool canMove = Unlocked(SkillUpgradeType.Shard_MoveToEnemy) || Unlocked(SkillUpgradeType.Shard_MultiCast);

        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        shard.GetComponent<SkillObject_Shard>().SetupShard(this, detonateTimer, canMove, shardSpeed);
    }

    public float GetDetonateTime()
    {
        switch (true)
        {
            case bool _ when Unlocked(SkillUpgradeType.Shard_Teleport):
            case bool _ when Unlocked(SkillUpgradeType.Shard_TeleportHpRewind):
                return shardExistDuration;

            default:
                return detonateTimer;
        }
    }

    private void ForceCooldown()
    {
        if(OnCooldown() == false)
        {
            SetSkillOnCooldown();
            currentShard.OnExplode -= ForceCooldown;
        }
    }
}
