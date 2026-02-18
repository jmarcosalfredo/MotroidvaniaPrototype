using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamageble
{
    private Slider healthBar;
    private Entity_VFX entityVFX;
    private Entity entity;

    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp = 100f;
    [SerializeField] protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7f, 7f);
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private float heavyKnockbackDuration = .5f;

    [Header("On Heavy Damage Knockback")]
    [SerializeField] private float heavyDamageThreshold = .3f; //Percentage of health you should lose to consider as heavy damage

    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        healthBar = GetComponentInChildren<Slider>();

        currentHp = maxHp;
        HandleHealthBar();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if(isDead)
        {
            return;
        }

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateDuration(damage);

        entity?.ReciveKnockBack(knockback, duration);
        entityVFX?.PlayOnDamageVfx();
        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        currentHp -= damage;
        HandleHealthBar();

        if(currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private void HandleHealthBar()
    {
        if(healthBar == null)
        {
            return;
        }

        healthBar.value = currentHp / maxHp;
    }


    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;
        knockback.x *= direction;

        return knockback;
    }

    private float CalculateDuration(float damage)
    {
        return IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    }

    private bool IsHeavyDamage(float damage)
    {
        return damage / maxHp > heavyDamageThreshold;
    }
}
