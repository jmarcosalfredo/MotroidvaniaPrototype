using UnityEngine;

public class UI_Craft : MonoBehaviour
{
    [SerializeField] private UI_ItemSlotParent inventoryParent;
    private Inventory_Player playerInventory;
    private UI_CraftPreview craftPreviewUI;
    private UI_CraftSlot[] craftSlots;
    private UI_CraftListButton[] craftListButtons;

    public void SetupCraftUI(Inventory_Storage playerStorage)
    {
        playerInventory = playerStorage.playerInventory;
        playerInventory.OnInventoryChange += UpdateUI;
        UpdateUI();


        craftPreviewUI = GetComponentInChildren<UI_CraftPreview>();
        craftPreviewUI.SetupCraftPreview(playerStorage);
        SetupCraftListButtons();
    }

    private void SetupCraftListButtons()
    {
        craftSlots = GetComponentsInChildren<UI_CraftSlot>();
        craftListButtons = GetComponentsInChildren<UI_CraftListButton>();

        foreach (var slot in craftSlots)
        {
            slot.gameObject.SetActive(false);
        }

        foreach (var button in craftListButtons)
        {
            button.SetCraftSlots(craftSlots);
        }
    }

    private void UpdateUI() => inventoryParent.UpdateSlots(playerInventory.itemList);
}
