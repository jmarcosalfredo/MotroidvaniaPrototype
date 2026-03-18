using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Item Effect/Heal on doing damage", fileName = "Item Effect Data - Heal on doing phys damage")]
public class ItemEffect_HealOnDoingDamage : ItemEffectDataSO
{
    [SerializeField] private float percentHealedOnAttack = 0.2f;

    public override void Subscribe(Player player)
    {
        base.Subscribe(player);
        player.combat.OnDoingPhysicalDamage += HealOnDoingDamage;
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        player.combat.OnDoingPhysicalDamage -= HealOnDoingDamage;
        player = null;
    }

    private void HealOnDoingDamage(float damage)
    {
        player.health.IncreaseHealth(damage * percentHealedOnAttack);
    }
}
