using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    public override bool TakeDamage(float damage, Transform damageDealer)
    {
        bool wasHit =base.TakeDamage(damage, damageDealer);
        
        if (wasHit == false)
        {
            return false;
        }
        
        if (damageDealer.GetComponent<Player>() != null) //can be damageDealer.CompareTag("Player") if the player has the "Player" tag
        {
            enemy.TryEnterBattleState(damageDealer);
        }

        return true;
    }
}
