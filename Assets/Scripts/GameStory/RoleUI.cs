using System;
using System.Collections;
using Global;
using UnityEngine;

public class RoleUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator Show(FadeController fadeController)
    {
        yield return fadeController.FadeIn(canvasGroup, 1f);

        yield return new WaitForSeconds(5f);
    }

    public IEnumerator Hide(FadeController fadeController)
    {
        yield return fadeController.FadeOut(canvasGroup, 1f);
    }
}
