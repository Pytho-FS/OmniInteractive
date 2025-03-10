using UnityEngine;
using System.Collections;
public class SceneLoadFade : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    private SpriteRenderer sr;

    void Start()
    {
        // Find the FadeScreen object and get its SpriteRenderer
        GameObject fadeScreenObj = GameObject.FindGameObjectWithTag("FadeScreen");

        if (fadeScreenObj != null)
        {
            sr = fadeScreenObj.GetComponent<SpriteRenderer>();

            if (sr != null)
            { 
                StartCoroutine(FadeRoutine(sr, 0f, true));
            }
            else
            {
                Debug.LogWarning("FadeScreen does not have a SpriteRenderer!");
            }
        }
        else
        {
            Debug.LogWarning("FadeScreen not found!");
        }
    }

    public IEnumerator FadeRoutine(SpriteRenderer sr, float targetAlpha, bool fadeIn)
    {
        float startAlpha = sr.color.a;
        float endAlpha = fadeIn ? targetAlpha : 0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            Color newColor = sr.color;
            newColor.a = newAlpha;
            sr.color = newColor;
            yield return null;
        }

        Color finalColor = sr.color;
        finalColor.a = endAlpha;
        sr.color = finalColor;
    }
}
