using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    private Entity_Stats playerStats;
    public List<Inventory_EquipmentSlot> equipList;

    protected override void Awake()
    {
        base.Awake();
        playerStats = GetComponent<Entity_Stats>();
    }

    public void TryEquipItem(Inventory_Item item)
    {
        Inventory_Item inventoryItem = FindItem(item.itemData);
        List<Inventory_EquipmentSlot> matchingSlots = equipList.FindAll(slot => slot.slotType == item.itemData.itemType);

        // TRY TO FIND AN EMPTY SLOT AND EQUIP ITEM
        foreach (var slot in matchingSlots)
        {
            if (slot.HasItem() == false)
            {
                EquipItem(inventoryItem, slot);
                return;
            }
        }

        // IF NO EMPTY SLOT, REPLACE FIRST ONE
        var slotsToReplace = matchingSlots[0];
        var itemToUnequip = slotsToReplace.equipedItem;

        EquipItem(inventoryItem, slotsToReplace);
        UnequipItem(itemToUnequip);
    }

    private void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot)
    {
        slot.equipedItem = itemToEquip;
        itemToEquip.AddModifiers(playerStats);

        RemoveItem(itemToEquip);
    }

    public void UnequipItem(Inventory_Item itemToUnequip)
    {
        if (CanAddItem() == false)
        {
            Debug.Log("Not enough space in inventory to unequip item");
            return;
        }

        foreach (var slot in equipList)
        {
            if (slot.equipedItem == itemToUnequip)
            {
                slot.equipedItem = null;
                break;
            }
        }

        itemToUnequip.RemoveModifiers(playerStats);
        AddItem(itemToUnequip);
    }
}
