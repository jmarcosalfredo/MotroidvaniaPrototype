using System.Collections.Generic;
using UnityEngine;

public class Player_DropManager : Entity_DropManager
{
    [Header("Player Drop Details")]
    [Range(0f, 100f)]
    [SerializeField] private float chanceToLooseItem = 90f;
    private Inventory_Player playerInventory;

    private void Awake()
    {
        playerInventory = GetComponent<Inventory_Player>();
    }

    public override void DropItems()
    {
        List<Inventory_Item> inventoryCopy = new List<Inventory_Item>(playerInventory.itemList);
        List<Inventory_EquipmentSlot> equipCopy = new List<Inventory_EquipmentSlot>(playerInventory.equipList);

        foreach (var item in inventoryCopy)
        {
            if(Random.Range(0f, 100f) <= chanceToLooseItem)
            {
                CreateItemToDrop(item.itemData);
                playerInventory.RemoveFullStack(item);
            }
        }

        foreach (var equip in equipCopy)
        {
            if( Random.Range(0f, 100f) <= chanceToLooseItem && equip.HasItem())
            {
                var item = equip.GetEquipedItem();

                CreateItemToDrop(item.itemData);
                playerInventory.UnequipItem(item);
                playerInventory.RemoveFullStack(item);
            }
        }
    }
}
