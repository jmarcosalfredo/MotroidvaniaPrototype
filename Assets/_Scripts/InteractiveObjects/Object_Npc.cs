using System;
using UnityEngine;

public class Object_Npc : MonoBehaviour
{
    protected Transform player;
    protected UI ui;

    [SerializeField] private Transform npc;
    [SerializeField] private GameObject interactTooltip;
    private bool facingRight = true;

    [Header("Floaty Tooltip")]
    [SerializeField] private float floatSpeed = 8f;
    [SerializeField] private float floatRange = 0.1f;
    private Vector3 tooltipStartPos;

    protected virtual void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        tooltipStartPos = interactTooltip.transform.position;
        interactTooltip.SetActive(false);
    }

    protected virtual void Update()
    {
        HandleNpcFlip();
        HandleTooltipFloat();
    }

    private void HandleTooltipFloat()
    {
        if(interactTooltip.activeSelf)
        {
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
            interactTooltip.transform.position = tooltipStartPos + new Vector3(0, yOffset);
        }
    }
    private void HandleNpcFlip()
    {
        if (player == null || npc == null)
        {
            return;
        }

        if (npc.position.x > player.position.x && facingRight)
        {
            npc.transform.Rotate(0, 180, 0);
            facingRight = false;
        }
        else if (npc.position.x < player.position.x && !facingRight)
        {
            npc.transform.Rotate(0, 180, 0);
            facingRight = true;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
       player = collision.transform;
       interactTooltip.SetActive(true);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        interactTooltip.SetActive(false);
    }
}
