using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Item Effect/Heal Effect", fileName = "Item Effect Data - Heal")]
public class ItemEffect_Heal : ItemEffectDataSO
{
    [SerializeField] private float healPercent = 0.1f;

    public override void ExecuteEffect()
    {
        Player player = FindFirstObjectByType<Player>();

        float healAmount = player.stats.GetMaxHealth() * healPercent;

        player.health.IncreaseHealth(healAmount);
    }
}
