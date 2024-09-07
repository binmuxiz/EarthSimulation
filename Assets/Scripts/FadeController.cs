using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    //TODO DoTween 사용하지 않고 구현해보기
    public IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        // canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
        yield return canvasGroup.DOFade(1f, duration).WaitForCompletion();
    }
    
    public IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        yield return canvasGroup.DOFade(0f, duration).WaitForCompletion();
        // canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
    }
}
