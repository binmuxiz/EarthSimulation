using System;
using System.Collections;
using Global;
using UnityEngine;

public class RoleUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private float fadeDuration = 0.6f;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator Show(FadeController fadeController)
    {
        yield return fadeController.FadeIn(canvasGroup, fadeDuration);

        yield return new WaitForSeconds(3f);
    }

    public IEnumerator Hide(FadeController fadeController)
    {
        yield return fadeController.FadeOut(canvasGroup, fadeDuration);
    }
}
