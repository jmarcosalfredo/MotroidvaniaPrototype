using TMPro;
using UnityEngine;
using System.Text;

public class UI_ItemToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemInfo;

    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private Transform merchantInfo;
    [SerializeField] private Transform inventoryInfo;

    public void ShowToolTip(bool show, RectTransform targetRect, Inventory_Item itemToShow, bool buyPrice = false, bool showMerchantInfo = false)
    {
        base.ShowToolTip(show, targetRect);

        merchantInfo.gameObject.SetActive(showMerchantInfo);
        inventoryInfo.gameObject.SetActive(!showMerchantInfo);

        int price = buyPrice ? itemToShow.buyPrice : Mathf.FloorToInt(itemToShow.sellPrice);
        int totalPrice = price * itemToShow.stackSize;

        string fullStackPrice = ($"Price: {price} x {itemToShow.stackSize} = {totalPrice}g.");
        string singlePrice = ($"Price: {price}g.");

        itemPrice.text = itemToShow.stackSize > 1 ? fullStackPrice : singlePrice;
        itemType.text = itemToShow.itemData.itemType.ToString();
        itemInfo.text = itemToShow.GetItemInfo();

        string color = GetColorByRarity(itemToShow.itemData.itemRarity);
        itemName.text = GetColoredText(color, itemToShow.itemData.itemName);
    }

    private string GetColorByRarity(int rarity)
    {
        switch (rarity)
        {
            case <= 100:
                return "#FFFFFF"; // Common - White
            case <= 300:
                return "#1e47ff"; // Uncommon - Blue
            case <= 600:
                return "#52f06c"; // Rare - Green
            case <= 900:
                return "#fffb00"; // Epic - Yellow
            default:
                return "#ff0000"; // Legendary - Red
        }
    }
}
