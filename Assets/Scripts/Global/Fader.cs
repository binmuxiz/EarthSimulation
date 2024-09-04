using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Global
{
    public class Fader : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float duration = 0.15f;

        private void Awake()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        }

        public IEnumerator Show()
        {
            canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
            // 트위닝이 끝날때까지 대기 
            yield return canvasGroup.DOFade(1f, duration).WaitForCompletion();
        }

        public IEnumerator Hide()
        {
            yield return canvasGroup.DOFade(0f, duration).WaitForCompletion();
            canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        }
    }
}
