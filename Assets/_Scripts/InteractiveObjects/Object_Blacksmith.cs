using UnityEngine;

public class Object_Blacksmith : Object_Npc, IInteractable
{
    private Animator anim;
    private Inventory_Player playerInventory;
    private Inventory_Storage storage;
    public void Interact()
    {
        ui.storageUI.SetupStorage(playerInventory , storage);
        ui.storageUI.gameObject.SetActive(true);
    }

    protected override void Awake()
    {
        base.Awake();
        storage = GetComponent<Inventory_Storage>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isBlacksmith", true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        playerInventory = player.GetComponent<Inventory_Player>();
        storage.SetInventory(playerInventory);
    }
}
