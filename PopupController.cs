using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PopupController : MonoBehaviour
{
    [Tooltip("How long the fade animation should take in seconds.")]
    public float fadeDuration = 0.4f;
    private Renderer objectRenderer;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        SetAlpha(0f);
    }

    void Start()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOutAndDestroy()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            SetAlpha(alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        SetAlpha(1f);
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startAlpha = objectRenderer.material.color.a;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, timer / fadeDuration);
            SetAlpha(alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    
    private void SetAlpha(float alpha)
    {
        if (objectRenderer != null && objectRenderer.material != null)
        {
            Color newColor = objectRenderer.material.color;
            newColor.a = alpha;
            objectRenderer.material.color = newColor;
        }
    }
}