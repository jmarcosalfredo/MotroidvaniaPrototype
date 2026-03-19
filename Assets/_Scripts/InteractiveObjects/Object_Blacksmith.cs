using UnityEngine;

public class Object_Blacksmith : Object_Npc, IInteractable
{
    private Animator anim;
    public void Interact()
    {
        Debug.Log("Interacted with blacksmith");
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isBlacksmith", true);
    }
}
