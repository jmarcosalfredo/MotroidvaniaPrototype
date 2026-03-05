using Unity.VisualScripting;
using UnityEngine;

public class SkillObject_SwordPierce : SkillObject_Sword
{
    private int pierceCount;

    public override void SetupSword(Skill_SwordThrow skill, Vector2 direction)
    {
        base.SetupSword(skill, direction);
        pierceCount = swordManager.pierceCount;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        bool groundHit = collision.gameObject.layer == LayerMask.NameToLayer("Ground");

        if (pierceCount <= 0 || groundHit)
        {
            DamageEnemiesInRadius(transform, 0.3f);
            StopSword(collision);
            return;
        }

        pierceCount--;
        DamageEnemiesInRadius(transform, 0.3f);
    }
}
