using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Global
{
    public class FadeController : Singleton<FadeController>                                                                                                                             
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        //TODO DoTween 사용하지 않고 구현해보기
        public static IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
            yield return canvasGroup.DOFade(1f, duration).WaitForCompletion();
        }
    
        public static IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
        {
            yield return canvasGroup.DOFade(0f, duration).WaitForCompletion();
            canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        }
    }
}
