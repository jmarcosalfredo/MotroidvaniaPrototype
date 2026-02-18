using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    public override void TakeDamage(float damage, Transform damageDealer)
    {
        base.TakeDamage(damage, damageDealer);
        
        if (isDead)
        {
            return;
        }
        
        if (damageDealer.GetComponent<Player>() != null) //can be damageDealer.CompareTag("Player") if the player has the "Player" tag
        {
            enemy.TryEnterBattleState(damageDealer);
        }

    }
}
