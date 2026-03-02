using System;
using UnityEngine;

public class SkillObject_Shard : SkillObject_Base
{

    public event Action OnExplode;
    private Skill_Shard shardManager;
    [SerializeField] private GameObject explosionVfxPrefab;

    private Transform target;
    private float speed;

    public void Update()
    {
        if (target == null)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void MoveTowardsClosestTarget(float speed)
    {
        target = ClosestTarget();
        this.speed = speed;
    }

    public void SetupShard(Skill_Shard shardManager)
    {
        this.shardManager = shardManager;

        playerStats = shardManager.player.stats;
        damageScaleData = shardManager.damageScaleData;

        float detonationTimer = shardManager.GetDetonateTime();

        Invoke(nameof(Explode), detonationTimer);
    }

    public void Explode()
    {
        DamageEnemiesInRadius(transform, checkRadius);
        Instantiate(explosionVfxPrefab, transform.position, Quaternion.identity);
        OnExplode?.Invoke();
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() == null)
        {
            return;

        }

        Explode();
    }
}
