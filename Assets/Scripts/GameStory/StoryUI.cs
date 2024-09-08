using System.Collections;
using Global;
using TMPro;
using UnityEngine;

namespace GameStory
{
    public class StoryUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public TMP_Text tmpText;
        public TextLoader textLoader;
        
        private float fadeDuration = 0.6f;

        private void Awake()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        }

        public IEnumerator Show(FadeController fadeController)
        {
            yield return fadeController.FadeIn(canvasGroup, fadeDuration);

            yield return textLoader.LoadText("game_story", tmpText, true);

            // TODO 로딩 아이콘
            yield return new WaitForSeconds(3f);

        }

        public IEnumerator Hide(FadeController fadeController)
        {
            yield return fadeController.FadeOut(canvasGroup, fadeDuration);
        }
    }
}
