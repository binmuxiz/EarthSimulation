using UnityEditor;
using UnityEngine;

namespace Home
{
    public class HomeMenu : MonoBehaviour
    {
        public void StartGame()
        {
            EffectSoundManager.Instance.ButtonEffect();
            StartCoroutine(HomeUIManager.Instance.HideMenu());
            // story 선택창
            StartCoroutine(HomeUIManager.Instance.ShowStorySelection());
        }

        public void QuitGame()
        {
            EffectSoundManager.Instance.ButtonEffect();

            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}