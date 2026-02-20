using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamageble
{
    private Entity entity;
    private Slider healthBar;
    private Entity_VFX entityVFX;
    private Entity_Stats entityStats;

    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead;
    [Header("Health Regen")]
    [SerializeField] private float regenInterval = 1f;
    [SerializeField] private bool canRegenHealth = true;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7f, 7f);
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private float heavyKnockbackDuration = .5f;

    [Header("On Heavy Damage Knockback")]
    [SerializeField] private float heavyDamageThreshold = .3f; //Percentage of health you should lose to consider as heavy damage

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entityVFX = GetComponent<Entity_VFX>();
        entityStats = GetComponent<Entity_Stats>();
        healthBar = GetComponentInChildren<Slider>();

        currentHealth = entityStats.GetMaxHealth();
        HandleHealthBar();

        InvokeRepeating(nameof(RegenerateHealth), 0, regenInterval);
    }

    public virtual bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer)
    {
        if (isDead)
        {
            return false;
        }

        if (AttackEvaded())
        {
            return false;
        }

        Entity_Stats attackerStats = damageDealer.GetComponent<Entity_Stats>();
        float armorReduction = attackerStats != null ? attackerStats.GetArmorReduction() : 0f;

        float mitigation = entityStats.GetArmorMitigation(armorReduction);
        float physicalDmgTaken = damage * (1 - mitigation);

        float resistance = entityStats.GetElementalResistance(element);
        float elementalDmgTaken = elementalDamage * (1 - resistance);

        TakeKnockback(damageDealer, physicalDmgTaken);
        ReduceHealth(physicalDmgTaken + elementalDmgTaken);

        return true;
    }

    private bool AttackEvaded()
    {
        return Random.Range(0f, 100f) < entityStats.GetEvasion();
    }

    private void RegenerateHealth()
    {
        if (canRegenHealth == false)
        {
            return;
        }

        float regenAmount = entityStats.resource.healthRegen.GetValue();
        IncreaseHealth(regenAmount);
    }

    public void IncreaseHealth(float healAmount)
    {
        if (isDead)
        {
            return;
        }

        float newHealth = currentHealth + healAmount;
        float maxHealth = entityStats.GetMaxHealth();

        currentHealth = Mathf.Min(newHealth, maxHealth);
        HandleHealthBar();
    }

    public void ReduceHealth(float damage)
    {
        entityVFX?.PlayOnDamageVfx();
        currentHealth -= damage;
        HandleHealthBar();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
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
        if (healthBar == null)
        {
            return;
        }

        healthBar.value = currentHealth / entityStats.GetMaxHealth();
    }

    private void TakeKnockback(Transform damageDealer, float finalDmg)
    {
        Vector2 knockback = CalculateKnockback(finalDmg, damageDealer);
        float duration = CalculateDuration(finalDmg);

        entity?.ReciveKnockBack(knockback, duration);
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
        return damage / entityStats.GetMaxHealth() > heavyDamageThreshold;
    }
}
