using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Global
{
    public class Fader : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float duration = 2f;

        private void Awake()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        }

        public IEnumerator Show()
        {
            yield return Show(duration);
        }
        
        public IEnumerator Show(float fadeDuration)
        {
            canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
            yield return canvasGroup.DOFade(1f, fadeDuration).WaitForCompletion(); // 트위닝이 끝날때까지 대기 
        }

        public IEnumerator Hide()
        {
            yield return Hide(duration);
        }
        
        public IEnumerator Hide(float fadeDuration)
        {
            yield return canvasGroup.DOFade(0f, fadeDuration).WaitForCompletion();
            canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        }
    }
}
