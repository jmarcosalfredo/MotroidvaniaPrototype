using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Data.SqlTypes;

public class UI_CraftPreview : MonoBehaviour
{
    private Inventory_Item itemToCraft;
    private Inventory_Storage playerStorage;
    private UI_CraftPreviewMaterialSlot[] craftPreviewSlots;

    [Header("Item Preview Setup")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemInfo;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void SetupCraftPreview(Inventory_Storage playerStorage)
    {
        this.playerStorage = playerStorage;
        craftPreviewSlots = GetComponentsInChildren<UI_CraftPreviewMaterialSlot>();

        foreach (var slot in craftPreviewSlots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public void ConfirmCraft()
    {
        if(itemToCraft == null)
        {
            buttonText.text = "Select an Item!";
            return;
        }

        if(playerStorage.HasEnoughMaterials(itemToCraft) && playerStorage.playerInventory.CanAddItem(itemToCraft))
        {
            playerStorage.ConsumeMaterial(itemToCraft);
            playerStorage.playerInventory.AddItem(itemToCraft);
        }

        UpdateCraftPreviewSlots();
    }

    public void UpdateCraftPreview(ItemDataSO itemData)
    {
        itemToCraft = new Inventory_Item(itemData);

        itemIcon.sprite = itemData.itemIcon;
        itemName.text = itemData.itemName;
        itemInfo.text = itemToCraft.GetItemInfo();
        UpdateCraftPreviewSlots();
    }

    public void UpdateCraftPreviewSlots()
    {
        foreach (var slot in craftPreviewSlots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = 0; i < itemToCraft.itemData.craftRecipe.Length; i++)
        {
            Inventory_Item requiredItem = itemToCraft.itemData.craftRecipe[i];
            int avaliableAmount = playerStorage.GetAvailableAmountOf(requiredItem.itemData);
            int requiredAmount = requiredItem.stackSize;

            craftPreviewSlots[i].gameObject.SetActive(true);
            craftPreviewSlots[i].SetupPreviewSlot(requiredItem.itemData, avaliableAmount, requiredAmount);
        }
    }
}
