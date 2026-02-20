using System.Collections;
using UnityEngine;

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Buff Details")]
    [SerializeField] private float buffDuration = 4f;
    [SerializeField] private bool canBeUsed = true;


    [Header("Floaty Movement")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatRange = 0.1f;
    private Vector3 startPosition;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        startPosition = transform.position;
    }

    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeUsed == false)
        {
            return;
        }

        StartCoroutine(BuffCo(buffDuration));
    }

    private IEnumerator BuffCo(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear;
        Debug.Log($"Buff Activated for {duration} seconds!");

        yield return new WaitForSeconds(duration);
        Debug.Log("Buff Deactivated!");
        Destroy(gameObject);
    }
}
