using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Data/Equipment Item", fileName = "Equipment Data - ")]
public class EquipmentDataSO : ItemDataSO
{
    [Header("Item Modifiers")]
    public ItemModifier[] modifiers;
}

[Serializable]
public class ItemModifier
{
    public StatType statType;
    public float value;
}
