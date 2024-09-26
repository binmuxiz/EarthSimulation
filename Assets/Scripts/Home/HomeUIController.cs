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
        public CanvasGroup introUI;
        public CanvasGroup menuUI;
        public CanvasGroup loginMenuUI;
        public CanvasGroup connectingUI;

        public CanvasGroup storySelectionUI;
        public Image storySelectionImage;
        public Sprite[] storySelectionSprites;
        
        public VideoHandler videoHandler;
        public RawImage screen;

        private void Awake()
        {
            videoHandler = GameObject.Find("Video Player").GetComponent<VideoHandler>();
            
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
            yield return FadeController.FadeOut(menuUI, 0f);
        }

        public IEnumerator ShowStorySelection()
        {
            yield return FadeController.FadeIn(storySelectionUI, 1.5f);
        }
        
        public IEnumerator HideStorySelection()
        {
            yield return FadeController.FadeOut(storySelectionUI, 0f);
        }
        
        public IEnumerator ShowLoginMenu()
        {
            yield return FadeController.FadeIn(loginMenuUI, 1f);
        }
        
        public IEnumerator HideLoginMenu()
        {
            yield return FadeController.FadeOut(loginMenuUI, 0f);
        }
        
        public async UniTask ShowConnectingView()
        {
            connectingUI.alpha = 1f;
            
            Debug.Log("ShowConnectingView()");
            
            await videoHandler.PrepareVideo(screen, VideoHandler.VideoType.Connecting);
            
            videoHandler.PlayVideo();
            
            await UniTask.WaitForSeconds(3f);
            
            //BGMManger.Instance.SoundChange(BGMManger.Bgm.WaitingRoom);
        }

        public void HideConnectingView()
        {
            videoHandler.StopVideo();
        }

        public void ShowSelectStoryUI(int idx)
        {
            if (storySelectionSprites[idx] != null)
            {
                storySelectionImage.sprite = storySelectionSprites[idx];
            }
        }

        public void InitializeSelectStoryUI()
        {
            if (storySelectionSprites[0] != null)
            {
                storySelectionImage.sprite = storySelectionSprites[0];
            }
        }
    }
}
