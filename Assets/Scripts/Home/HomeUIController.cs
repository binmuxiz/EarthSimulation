using System.Collections;
using Cysharp.Threading.Tasks;
using Global;
using Handler;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Home
{
    public class HomeUIController : Singleton<HomeUIController>
    {
        [SerializeField] CanvasGroup introUI;
        [SerializeField] CanvasGroup menuUI;
        [SerializeField] CanvasGroup storySelectionUI;
        [SerializeField] CanvasGroup loginMenuUI;
        [SerializeField] CanvasGroup connectingUI;
        
        [SerializeField] VideoHandler videoHandler;
        [SerializeField] RawImage screen;

        private void Awake()
        {
            introUI.gameObject.SetActive(true);
            menuUI.gameObject.SetActive(true);
            storySelectionUI.gameObject.SetActive(true);
            loginMenuUI.gameObject.SetActive(true);
            connectingUI.gameObject.SetActive(true);

            introUI.alpha = 0f;
            menuUI.alpha = 0f;
            storySelectionUI.alpha = 0f;
            loginMenuUI.alpha = 0f;
            connectingUI.alpha = 0f;

            introUI.interactable = introUI.blocksRaycasts = false;
            menuUI.interactable = menuUI.blocksRaycasts = false;
            storySelectionUI.interactable = storySelectionUI.blocksRaycasts = false;
            loginMenuUI.interactable = loginMenuUI.blocksRaycasts = false;
            connectingUI.interactable = connectingUI.blocksRaycasts = false;

        }

        public IEnumerator ShowIntro()
        {
            yield return FadeController.FadeIn(introUI, 1f);
        }
        
        public IEnumerator HideIntro()
        {
            yield return FadeController.FadeOut(introUI, 1f);
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
        
        public IEnumerator ShowLoginMenu()
        {
            yield return FadeController.FadeIn(loginMenuUI, 1f);
        }
        
        public IEnumerator HideLoginMenu()
        {
            yield return FadeController.FadeOut(loginMenuUI, 1f);
        }
        
        public async UniTask ShowConnectingView()
        {
            connectingUI.alpha = 1f;
            
            Debug.Log("ShowConnectingView()");
            
            await videoHandler.PrepareVideo(screen, VideoHandler.VideoType.Connecting);
            
            videoHandler.PlayVideo();
            
            await UniTask.WaitForSeconds(3f);
        }

        public void HideConnectingView()
        {
            videoHandler.StopVideo();
        }
    }
}
