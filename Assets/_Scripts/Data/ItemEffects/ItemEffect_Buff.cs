using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Item Effect/Buff Effect", fileName = "Item Effect Data - Buff Effect")]
public class ItemEffect_Buff : ItemEffectDataSO
{
    [SerializeField] private BuffEffectData[] buffsToApply;
    [SerializeField] private float buffDuration;
    [SerializeField] private string source = Guid.NewGuid().ToString();

    public override bool CanBeUsed(Player player)
    {

        if (player.stats.CanApplyBuff(source))
        {
            this.player = player;
            return true;
        }
        else
        {
            Debug.Log("Buff already active, cannot use item.");
            return false;
        }
    }

    public override void ExecuteEffect()
    {
        player.stats.ApplyBuff(buffsToApply, buffDuration, source);
    }
}
