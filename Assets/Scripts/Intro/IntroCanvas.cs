using System;
using System.Collections;
using DG.Tweening;
using Global;
using UnityEngine;

namespace Intro
{
    public class IntroCanvas : MonoBehaviour
    {
        public CanvasGroup introCanvasGroup;

        [SerializeField] private float fadeDuration = 1.5f;

        private void Awake()
        {
            introCanvasGroup.alpha = 0f;
        }

        private IEnumerator Start()
        {
            yield return introCanvasGroup.DOFade(1f, fadeDuration).WaitForCompletion();
            
            yield return new WaitForSeconds(1.3f);

            SceneManager.Instance.LoadScene("Main Scene");
        }
    }
}
