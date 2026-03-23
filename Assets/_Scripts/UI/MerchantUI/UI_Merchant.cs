using UnityEngine;

public class UI_Merchant : MonoBehaviour
{
    private Inventory_Player playerInventory;
    private Inventory_Merchant merchantInventory;

    [SerializeField] private UI_ItemSlotParent merchantSlots;
    [SerializeField] private UI_ItemSlotParent playerSlots;
    [SerializeField] private UI_EquipSlotParent equipSlots;

    public void StupMerchantUI(Inventory_Merchant merchantInventory, Inventory_Player playerInventory)
    {
        this.merchantInventory = merchantInventory;
        this.playerInventory = playerInventory;

        this.playerInventory.OnInventoryChange += UpdateSlotUI;
        UpdateSlotUI();

        UI_MerchantSlot[] merchantSlots = GetComponentsInChildren<UI_MerchantSlot>();
        foreach (var slot in merchantSlots)
        {
            slot.SetupMerchantUI(merchantInventory);
        }
    }

    private void UpdateSlotUI()
    {
        if (playerInventory == null)
        {
            return;
        }

        merchantSlots.UpdateSlots(merchantInventory.itemList);
        playerSlots.UpdateSlots(playerInventory.itemList);
        equipSlots.UpdateEuipmentSlots(playerInventory.equipList);
    }
}
