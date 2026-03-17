using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Item Effect/Buff Effect", fileName = "Item Effect Data - Buff Effect")]
public class ItemEffect_Buff : ItemEffectDataSO
{
    [SerializeField] private BuffEffectData[] buffsToApply;
    [SerializeField] private float buffDuration;
    [SerializeField] private string source = Guid.NewGuid().ToString();

    private Player_Stats playerStats;

    public override bool CanBeUsed()
    {
        if (playerStats == null)
        {
            playerStats = FindFirstObjectByType<Player_Stats>();
        }

        if (playerStats.CanApplyBuff(source))
        {
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
        playerStats.ApplyBuff(buffsToApply, buffDuration, source);
    }
}
