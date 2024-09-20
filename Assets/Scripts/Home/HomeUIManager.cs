using System.Collections;
using Global;
using UnityEngine;

namespace Home
{
    public class HomeUIManager : Singleton<HomeUIManager>
    {
        [SerializeField] CanvasGroup introUI;
        [SerializeField] CanvasGroup menuUI;
        [SerializeField] CanvasGroup storySelectionUI;
        [SerializeField] CanvasGroup miniMenuUI;
        [SerializeField] GameObject loadingUI;

        private FadeController _fadeController;
        
        private void Awake()
        {
            _fadeController = FadeController.Instance;
            
            introUI.gameObject.SetActive(true);
            menuUI.gameObject.SetActive(true);
            storySelectionUI.gameObject.SetActive(true);
            miniMenuUI.gameObject.SetActive(true);
            
            loadingUI.SetActive(false);

            introUI.alpha = 0f;
            menuUI.alpha = 0f;
            storySelectionUI.alpha = 0f;
            miniMenuUI.alpha = 0f;

            introUI.interactable = introUI.blocksRaycasts = false;
            menuUI.interactable = menuUI.blocksRaycasts = false;
            storySelectionUI.interactable = storySelectionUI.blocksRaycasts = false;
            miniMenuUI.interactable = miniMenuUI.blocksRaycasts = false;
            
        }

        private IEnumerator Start()
        {
            yield return _fadeController.FadeIn(introUI, 1f);
            yield return new WaitForSeconds(2f);
            yield return _fadeController.FadeOut(introUI, 1f);

            yield return ShowMenu();
            introUI.gameObject.SetActive(false);
        }
        
        public IEnumerator ShowMenu()
        {
            yield return _fadeController.FadeIn(menuUI, 1f);
        }

        public IEnumerator HideMenu()
        {
            yield return _fadeController.FadeOut(menuUI, 1f);
        }

        public IEnumerator ShowStorySelection()
        {
            yield return _fadeController.FadeIn(storySelectionUI, 1f);
        }
        
        public IEnumerator HideStorySelection()
        {
            yield return _fadeController.FadeOut(storySelectionUI, 1f);
        }
        
        public IEnumerator ShowMiniMenu()
        {
            yield return _fadeController.FadeIn(miniMenuUI, 1f);
        }
        
        public IEnumerator HideMiniMenu()
        {
            yield return _fadeController.FadeOut(miniMenuUI, 1f);
        }
        
        public void ShowLoadingView()
        {
            loadingUI.SetActive(true);
        }
    }
}
