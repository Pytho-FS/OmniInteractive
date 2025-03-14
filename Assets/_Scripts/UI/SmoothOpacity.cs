using System.Collections;
using UnityEngine;

public class SmoothOpacity : MonoBehaviour
{
    [SerializeField] float fadeDuration = 1f;
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
