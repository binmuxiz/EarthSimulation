using System;
using System.Collections;
using Global;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WebSocketSharp;

namespace Home
{
    public class MiniMenu : Singleton<MiniMenu>
    {
        public GameStory GameStory { get; set; }

        [SerializeField] TMP_InputField nickName;
        [SerializeField] TMP_InputField roomName;


        void Awake()
        {
            nickName.text = "";
            roomName.text = "";
        }

        public void StartGame()
        {
            if (nickName.text.IsNullOrEmpty())
            {
                Debug.Log("닉네임을 입력하세요");
                return;
            }
            
            StartCoroutine(HomeUIManager.Instance.HideMiniMenu());
            HomeUIManager.Instance.ShowLoadingView();
            
            FusionManager.Instance.StartGame(roomName.text, GameStory);
        }
        
    }
}