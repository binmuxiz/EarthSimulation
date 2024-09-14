using System.Collections;
using Global;
using TMPro;
using UnityEngine;

namespace GameStory
{
    public class GameIntroUIController : MonoBehaviour
    {
        public CanvasGroup storyCanvasGroup;
        public CanvasGroup roleCanvasGroup;

        public TMP_Text storyText;
        public TextLoader textLoader;
        
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
            SharedData.Instance.CheckReadStoryDoneRpc();
            Debug.Log("tq");
            yield return new WaitUntil(() => SharedData.MaxCount <= SharedData.CountReadStoryDone);
            Debug.Log("모두 다 읽음");
            Debug.Log(UIManager.storyPermitted);
           
            SharedData.Instance.ClearReadCountRpc();
            //if(RunnerController.Runner.IsSceneAuthority)

        }

        private IEnumerator ShowStory()
        {
            yield return FadeController.Instance.FadeIn(storyCanvasGroup, _fadeDuration);
            yield return textLoader.LoadText("game_story", storyText, true);

            yield return new WaitForSeconds(3f);

            yield return FadeController.Instance.FadeOut(storyCanvasGroup, _fadeDuration);
        }

        private IEnumerator ShowRole()
        {
            yield return FadeController.Instance.FadeIn(roleCanvasGroup, _fadeDuration);
            yield return new WaitForSeconds(3f);
            yield return FadeController.Instance.FadeOut(roleCanvasGroup, _fadeDuration);
        }
        
    }
}
