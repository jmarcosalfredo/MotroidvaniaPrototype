using UnityEngine;

public class SkillObject_SwordSpin : SkillObject_Sword
{
    private int maxDistance;
    private float attacksPerSecond;
    private float attackTimer;

    public override void SetupSword(Skill_SwordThrow skill, Vector2 direction)
    {
        base.SetupSword(skill, direction);

        anim?.SetTrigger("spin");

        maxDistance = swordManager.maxDistance;
        attacksPerSecond = swordManager.attacksPerSecond;

        Invoke(nameof(GetSwordBackToPlayer), swordManager.maxSpinDurarion);
    }

    protected override void Update()
    {
        HandleAttack();
        HandleStopping();
        HandleComeback();
    }

    private void HandleStopping()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if(distanceToPlayer > maxDistance && rb.simulated == true)
        {
            rb.simulated = false;
        }
    }

    private void HandleAttack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer < 0f)
        {
            DamageEnemiesInRadius(transform, 1f);
            attackTimer = 1f / attacksPerSecond;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        rb.simulated = false;
    }
}
