using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MerchantSlot : UI_ItemSlot
{
    private Inventory_Merchant merchant;
    public enum MerchantSlotType { MerchantSlot, PlayerSlot }
    public MerchantSlotType slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot == null)
        {
            return;
        }

        bool rightButton = eventData.button == PointerEventData.InputButton.Right;
        bool leftButton = eventData.button == PointerEventData.InputButton.Left;

        if (slotType == MerchantSlotType.PlayerSlot)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Right:
                    bool sellFullStack = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
                    merchant.TrySellItem(itemInSlot, sellFullStack);
                    break;
                case PointerEventData.InputButton.Left:
                    base.OnPointerDown(eventData);
                    break;
            }
        }
        else if (slotType == MerchantSlotType.MerchantSlot)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Right:
                    bool buyFullStack = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
                    merchant.TryBuyItem(itemInSlot, buyFullStack);
                    break;
                case PointerEventData.InputButton.Left:
                    return;
            }
        }

        ui.itemToolTip.ShowToolTip(false, null);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (itemInSlot == null)
        {
            return;
        }

        if (slotType == MerchantSlotType.MerchantSlot)
        {
            ui.itemToolTip.ShowToolTip(true, rect, itemInSlot, true, true);
        }
        else
        {
            ui.itemToolTip.ShowToolTip(true, rect, itemInSlot, false, true);
        }
    }

    public void SetupMerchantUI(Inventory_Merchant merchant) => this.merchant = merchant;
    
}
