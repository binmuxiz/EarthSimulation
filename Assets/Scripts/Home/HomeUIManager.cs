using System.Collections;
using Global;
using UnityEngine;

namespace Home
{
    public class HomeUIManager : MonoBehaviour
    {
        public CanvasGroup introUI;
        public CanvasGroup homeUI;
        
        private void Awake()
        {
            introUI.gameObject.SetActive(true);
            homeUI.gameObject.SetActive(true);

            introUI.alpha = 0f;
            homeUI.alpha = 0f;

            introUI.interactable = introUI.blocksRaycasts = false;
            homeUI.interactable = homeUI.blocksRaycasts = false;
        }

        private IEnumerator Start()
        {
            yield return ShowIntro();
            yield return ShowHome();
        }
        
        private IEnumerator ShowIntro()
        {
            yield return FadeController.Instance.FadeIn(introUI, 2f);
            yield return new WaitForSeconds(2f);
            yield return FadeController.Instance.FadeOut(introUI, 2f);
        }
        
        private IEnumerator ShowHome()
        {
            yield return FadeController.Instance.FadeIn(homeUI, 2f);
        }
    }
}
