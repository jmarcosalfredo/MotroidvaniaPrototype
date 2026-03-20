using UnityEngine;

public class UI_Storage : MonoBehaviour
{
    private Inventory_Player inventoryPlayer;
    private Inventory_Storage storage;

    [SerializeField] private UI_ItemSlotParent playerInventorySlotsParent;
    [SerializeField] private UI_ItemSlotParent storageSlotsParent;
    [SerializeField] private UI_ItemSlotParent materialStashSlotsParent;

    public void SetupStorage(Inventory_Player inventoryPlayer, Inventory_Storage storage)
    {
        this.inventoryPlayer = inventoryPlayer;
        this.storage = storage;
        storage.OnInventoryChange += UpdateUI;
        UpdateUI();

        UI_StorageSlot[] storageSlots = GetComponentsInChildren<UI_StorageSlot>();

        foreach (var slot in storageSlots)
        {
            slot.SetStorage(storage);
        }
    }

    private void UpdateUI()
    {
        playerInventorySlotsParent.UpdateSlots(inventoryPlayer.itemList);
        storageSlotsParent.UpdateSlots(storage.itemList);
        materialStashSlotsParent.UpdateSlots(storage.materialStash);
    }
}
