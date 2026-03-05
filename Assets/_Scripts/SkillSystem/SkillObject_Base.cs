using Unity.VisualScripting;
using UnityEngine;

public class SkillObject_Base : MonoBehaviour
{
    [SerializeField] private GameObject onHitVfxPrefab;
    [Space]
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected Transform targetCheck;
    [SerializeField] protected float checkRadius = 1f;

    protected Animator anim;
    protected Entity_Stats playerStats;
    protected ScaleEffectData damageScaleData;
    protected ElementType usedElement;
    protected bool targetGotHit;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected void DamageEnemiesInRadius(Transform t, float radius)
    {
        foreach (var target in GetEnemiesAround(t, radius))
        {
            IDamageble damageble = target.GetComponent<IDamageble>();

            if (damageble == null)
            {
                continue;
            }

            AttackData attackData = playerStats.GetAttackData(damageScaleData);
            Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

            float physDamage = attackData.physicalDamage;
            float elemDamage = attackData.elementalDamage;
            ElementType element = attackData.element;
            bool isCrit = attackData.isCrit;

            targetGotHit = damageble.TakeDamage(physDamage, elemDamage, element, transform);

            if (element != ElementType.None)
            {
                statusHandler?.ApplyStatusEffect(element, attackData.effectData);
            }

            if (targetGotHit)
            {
                Instantiate(onHitVfxPrefab, target.transform.position, Quaternion.identity);
            }

            usedElement = element;
        }
    }

    protected Transform ClosestTarget()
    {
        Transform target = null;
        float closestDistance = Mathf.Infinity;
        foreach (var enemy in GetEnemiesAround(transform, 10f))
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                target = enemy.transform;
                closestDistance = distance;
            }
        }
        return target;
    }

    protected Collider2D[] GetEnemiesAround(Transform t, float radius)
    {
        return Physics2D.OverlapCircleAll(t.position, radius, whatIsEnemy);
    }

    protected virtual void OnDrawGizmos()
    {
        if (targetCheck == null)
        {
            targetCheck = transform;
        }

        Gizmos.DrawWireSphere(targetCheck.position, checkRadius);
    }
}
