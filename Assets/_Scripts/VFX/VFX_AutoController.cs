using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1f;
    [Space]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;

    [Header("Fade Effect")]
    [SerializeField] private bool canFade;
    [SerializeField] private float fadeSpeed = 1f;

    [Header("Random Rotation")]
    [SerializeField] private float minRotation = 0f;
    [SerializeField] private float maxRotation = 360f;

    [Header("Random Position")]
    [SerializeField] private float xMinOffset = -0.3f;
    [SerializeField] private float xMaxOffset = 0.3f;
    [Space]
    [SerializeField] private float yMinOffset = -0.3f;
    [SerializeField] private float yMaxOffset = 0.3f;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (canFade)
        {
            StartCoroutine(FadeCo());
        }

        ApplyRandomOffset();
        ApplyRandomRotation();

        if(autoDestroy)
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    private IEnumerator FadeCo()
    {
        Color targetColor = Color.white;

        while(targetColor.a > 0f)
        {
            targetColor.a -= fadeSpeed * Time.deltaTime;
            sr.color = targetColor;
            yield return null;
        }

        sr.color = targetColor;
    }

    private void ApplyRandomOffset()
    {
        if (randomOffset == false)
        {
            return;
        }

        float xOffset = Random.Range(xMinOffset, xMaxOffset);
        float yOffset = Random.Range(yMinOffset, yMaxOffset);

        transform.position += new Vector3(xOffset, yOffset);
    }

    private void ApplyRandomRotation()
    {
        if (randomRotation == false)
        {
            return;
        }

        float zRotation = Random.Range(minRotation, maxRotation);
        transform.Rotate(0f, 0f, zRotation);
    }
}
