using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GameStory
{
    public class StoryUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public TMP_Text tmpText;
        public TextLoader textLoader;
        
        private void Awake()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        }
        
        private IEnumerator Start()
        {
            // TODO WaitForCompletion??
            // yield return canvasGroup.DOFade( 1f, 1f).WaitForCompletion();
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = canvasGroup.blocksRaycasts = true;

            yield return textLoader.LoadText("game_story", tmpText, true);
            // yield return textLoader.LoadText("game_story", tmpText, false);

            // TODO 로딩 아이콘
            yield return new WaitForSeconds(3f);

            canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
            yield return canvasGroup.DOFade( 0f, 1f).WaitForCompletion();
        }
    }
}
