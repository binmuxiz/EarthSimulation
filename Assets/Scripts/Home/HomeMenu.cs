using System.Collections;
using Global;
using UnityEditor;
using UnityEngine;

namespace Home
{
    public class HomeMenu : Singleton<HomeMenu>
    {
        public IEnumerator StartGame(string RoomName = null)
        {
            yield return HomeUIManager.Instance.HideMenu();
            // story 선택창
            yield return HomeUIManager.Instance.ShowStorySelection();
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