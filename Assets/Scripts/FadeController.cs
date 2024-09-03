using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private float duration = 2f;
    
    //TODO DoTween 사용하지 않고 구현해보기
    public IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        // canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
        yield return canvasGroup.DOFade(1f, duration).WaitForCompletion();
    }
    
    public IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        yield return canvasGroup.DOFade(0f, duration).WaitForCompletion();
        // canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
    }
}
