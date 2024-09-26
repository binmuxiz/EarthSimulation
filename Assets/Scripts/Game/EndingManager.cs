using System;
using Cysharp.Threading.Tasks;
using Global;
using Handler;
using Network;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Game
{
    public class EndingManager : Singleton<EndingManager>
    {
        public bool processPermitted = false;
        public VideoHandler vh;
        public GameObject curtain;
        
        
        public GameObject EndingUICanvas;
        public RawImage EndingVideoScreen;

        private void Awake()
        {
            curtain.SetActive(false);
            EndingUICanvas.SetActive(false);
            vh = GameObject.Find("Video Player").GetComponent<VideoHandler>();
        }

        private async void Start()
        {
            await UniTask.WaitUntil(() => processPermitted);
            StoryProcess();
        }
        
        private async void StoryProcess()
        {
            Debug.Log("Ending Process()");
            if (RunnerController.Runner.IsSharedModeMasterClient)
            {
                int data = await NetworkManager.Instance.RequestEnding();
                Debug.Log("ending idx : " + data);
                SharedData.Instance.RpcEndingIndex(data);
            }
            else 
            {
                await UniTask.WaitUntil(() => SharedData.EndingIndex != -1);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            
            Debug.Log($"Ending Index 결과 : {SharedData.EndingIndex}");

            Debug.Log("영상 재생");

            //영상 띄우기
            EndingVideoPlay(SharedData.EndingIndex);
            
            

        }
        
        private async void EndingVideoPlay(int videoIndex)
        {
            curtain.SetActive(true);
            int temp = videoIndex + 2;
            await vh.PrepareVideo(EndingVideoScreen, (VideoHandler.VideoType)temp);
            Debug.Log((VideoHandler.VideoType)temp);
            vh.PlayVideo();
            EndingUICanvas.SetActive(true);
        }
    }
}