using System.Collections;
using Global;
using UnityEngine;
using UnityEngine.Serialization;

namespace Home
{
    public class HomeUIManager : Singleton<HomeUIManager>
    {
        [SerializeField] CanvasGroup introUI;
        [SerializeField] CanvasGroup menuUI;
        [SerializeField] CanvasGroup storySelectionUI;
        [SerializeField] CanvasGroup gameStartMenuUI;
        [SerializeField] GameObject loadingUI;

        private void Awake()
        {
            introUI.gameObject.SetActive(true);
            menuUI.gameObject.SetActive(true);
            storySelectionUI.gameObject.SetActive(true);
            gameStartMenuUI.gameObject.SetActive(true);
            
            loadingUI.SetActive(false);

            introUI.alpha = 0f;
            menuUI.alpha = 0f;
            storySelectionUI.alpha = 0f;
            gameStartMenuUI.alpha = 0f;

            introUI.interactable = introUI.blocksRaycasts = false;
            menuUI.interactable = menuUI.blocksRaycasts = false;
            storySelectionUI.interactable = storySelectionUI.blocksRaycasts = false;
            gameStartMenuUI.interactable = gameStartMenuUI.blocksRaycasts = false;
            
        }

        private IEnumerator Start()
        {
            yield return FadeController.FadeIn(introUI, 1f);
            yield return new WaitForSeconds(2f);
            yield return FadeController.FadeOut(introUI, 1f);

            yield return ShowMenu();
            introUI.gameObject.SetActive(false);
        }
        
        public IEnumerator ShowMenu()
        {
            yield return FadeController.FadeIn(menuUI, 1f);
        }

        public IEnumerator HideMenu()
        {
            yield return FadeController.FadeOut(menuUI, 1f);
        }

        public IEnumerator ShowStorySelection()
        {
            yield return FadeController.FadeIn(storySelectionUI, 1f);
        }
        
        public IEnumerator HideStorySelection()
        {
            yield return FadeController.FadeOut(storySelectionUI, 1f);
        }
        
        public IEnumerator ShowGameStartMenu()
        {
            yield return FadeController.FadeIn(gameStartMenuUI, 1f);
        }
        
        public IEnumerator HideGameStartMenu()
        {
            yield return FadeController.FadeOut(gameStartMenuUI, 1f);
        }
        
        public void ShowLoadingView()
        {
            loadingUI.SetActive(true);
        }
    }
}
