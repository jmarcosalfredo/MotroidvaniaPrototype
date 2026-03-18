using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Item Effect/Ice Blast", fileName = "Item Effect Data - Ice Blast on taking damage")]
public class ItemEffect_IceBlastOnTakingDamage : ItemEffectDataSO
{
    [SerializeField] private ElementalEffectData effectData;
    [SerializeField] private float iceDamage;
    [SerializeField] LayerMask whatIsEnemy;

    [Space]
    [SerializeField] private float healthPercentTrigger = 0.25f;
    [SerializeField] private float cooldown;
    private float lastTimeUsed = -999f;

    [Header("Vfx Objects")]
    [SerializeField] private GameObject iceBlastVfx;
    [SerializeField] private GameObject onHitVfx;

    public override void ExecuteEffect()
    {
        bool noCooldown = Time.time >= lastTimeUsed + cooldown;
        bool reachedHealthThreshold = player.health.GetHealthPercent() <= healthPercentTrigger;

        if (noCooldown && reachedHealthThreshold)
        {
            player.vfx.CreateEffectOf(iceBlastVfx, player.transform);
            lastTimeUsed = Time.time;
            DamageEnemiesWithIceBlast();
        }
    }

    private void DamageEnemiesWithIceBlast()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(player.transform.position, 1.5f, whatIsEnemy);

        foreach (var target in enemies)
        {
            IDamageble damageble = target.GetComponent<IDamageble>();

            if (damageble == null)
            {
                continue;
            }

            bool targetGotHit = damageble.TakeDamage(0, iceDamage, ElementType.Ice, player.transform);

            Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();
            statusHandler?.ApplyStatusEffect(ElementType.Ice, effectData);

            if (targetGotHit)
            {
                player.vfx.CreateEffectOf(onHitVfx, target.transform);
            }
        }
    }

    public override void Subscribe(Player player)
    {
        base.Subscribe(player);
        player.health.OnTakingDamage += ExecuteEffect;
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        player.health.OnTakingDamage -= ExecuteEffect;
        player = null;
    }
}
