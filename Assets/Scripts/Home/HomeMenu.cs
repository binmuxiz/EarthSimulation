using System.Collections;
using Global;
using UnityEditor;
using UnityEngine;

namespace Home
{
    public class HomeMenu : Singleton<HomeMenu>
    {
        public void StartGame()
        {
            StartCoroutine(HomeUIManager.Instance.HideMenu());
            // story 선택창
            StartCoroutine(HomeUIManager.Instance.ShowStorySelection());
        }

        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}