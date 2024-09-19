using System.Collections;
using Global;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Home
{
    public class MiniMenu : Singleton<MiniMenu>
    {
        public int StoryNum { get; set; }

        [SerializeField] TMP_InputField nickName;
        [SerializeField] TMP_InputField roomName;

        public void StartGame()
        {
            StartCoroutine(HomeUIManager.Instance.HideMiniMenu());
            HomeUIManager.Instance.ShowLoadingView();
            
            FusionManager.Instance.StartGame();
        }
        
    }
}