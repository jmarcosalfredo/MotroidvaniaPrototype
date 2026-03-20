using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI_CraftPreviewMaterialSlot : MonoBehaviour
{
    [SerializeField] private Image materialIcon;
    [SerializeField] private TextMeshProUGUI materialNameValue;

    public void SetupPreviewSlot(ItemDataSO itemData, int avalableAmount, int requiredAmount)
    {
        materialIcon.sprite = itemData.itemIcon;
        materialNameValue.text = itemData.itemName + " - " + avalableAmount + "/" + requiredAmount;
    }
}
