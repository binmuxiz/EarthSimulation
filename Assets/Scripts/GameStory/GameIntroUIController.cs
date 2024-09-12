using System.Collections;
using System.Net.Mime;
using Global;
using TMPro;
using UnityEngine;
using SceneManager = Global.SceneManager;

namespace GameStory
{
    public class GameIntroUIController : MonoBehaviour
    {
        public CanvasGroup storyCanvasGroup;
        public CanvasGroup roleCanvasGroup;

        public TMP_Text storyText;
        public TextLoader textLoader;
        
        public FadeController fadeController;

        private float _fadeDuration = 0.6f;

        private void Awake()
        {
            textLoader = GetComponent<TextLoader>();
            
            storyCanvasGroup.gameObject.SetActive(true);
            roleCanvasGroup.gameObject.SetActive(true);

            storyCanvasGroup.alpha = 0f;
            roleCanvasGroup.alpha = 0f;

            storyCanvasGroup.interactable = storyCanvasGroup.blocksRaycasts = false;
            roleCanvasGroup.interactable = roleCanvasGroup.blocksRaycasts = false;
            
        }

        private IEnumerator Start()
        {
            yield return ShowStory();
            yield return new WaitForSeconds(1f);
            yield return ShowRole();
            SceneManager.Instance.LoadScene("Game Scene");
        }

        private IEnumerator ShowStory()
        {
            yield return fadeController.FadeIn(storyCanvasGroup, _fadeDuration);
            yield return textLoader.LoadText("game_story", storyText, true);

            yield return new WaitForSeconds(3f);

            yield return fadeController.FadeOut(storyCanvasGroup, _fadeDuration);
        }

        private IEnumerator ShowRole()
        {
            yield return fadeController.FadeIn(roleCanvasGroup, _fadeDuration);
            yield return new WaitForSeconds(3f);
            yield return fadeController.FadeOut(roleCanvasGroup, _fadeDuration);
        }
        
    }
}
