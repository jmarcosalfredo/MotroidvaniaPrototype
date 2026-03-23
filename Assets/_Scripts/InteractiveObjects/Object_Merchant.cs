using UnityEngine;

public class Object_Merchant : Object_Npc, IInteractable
{
    private Inventory_Player inventory;
    private Inventory_Merchant merchantInventory;

    protected override void Awake()
    {
        base.Awake();
        merchantInventory = GetComponent<Inventory_Merchant>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            merchantInventory.FillShopList();
        }
    }

    public void Interact()
    {
        ui.merchantUI.StupMerchantUI(merchantInventory, inventory);
        ui.merchantUI.gameObject.SetActive(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        inventory = player.GetComponent<Inventory_Player>();
        merchantInventory.SetInventory(inventory);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        ui.SwitchOffAllToolTips();
        ui.merchantUI.gameObject.SetActive(false);
    }
}
