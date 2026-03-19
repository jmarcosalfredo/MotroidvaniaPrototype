using UnityEngine;

public class Object_Merchant : Object_Npc, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted with merchant");
    }
}
