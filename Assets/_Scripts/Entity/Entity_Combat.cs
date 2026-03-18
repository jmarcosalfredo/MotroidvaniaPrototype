using System;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public event Action<float> OnDoingPhysicalDamage;

    private Entity_VFX vfx;
    private Entity_Stats stats;

    public ScaleEffectData basicAttackScale;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void PerformAttack()
    {
        GetDetectedColliders();

        foreach (var target in GetDetectedColliders())
        {
            IDamageble damageble = target.GetComponent<IDamageble>();

            if (damageble == null)
            {
                continue;
            }

            AttackData attackData = stats.GetAttackData(basicAttackScale);
            Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

            float physicalDamage = attackData.physicalDamage;
            float elementalDamage = attackData.elementalDamage;
            ElementType element = attackData.element;
            bool isCrit = attackData.isCrit;

            bool targetGotHit = damageble.TakeDamage(physicalDamage, elementalDamage, element, transform);

            if (element != ElementType.None)
            {
                statusHandler?.ApplyStatusEffect(element, attackData.effectData);
            }

            if (targetGotHit)
            {
                OnDoingPhysicalDamage?.Invoke(physicalDamage);
                vfx.CreateOnHitVFX(target.transform, isCrit, element);
            }
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
