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
        private GameObject _videoPlayer;
        public CanvasGroup FadeScreen;
        
        
        public GameObject EndingUICanvas;
        public RawImage EndingVideoScreen;

        private void Awake()
        {
            _videoPlayer = GameObject.Find("Video Player");
            vh = _videoPlayer.GetComponent<VideoHandler>();
            EndingUICanvas.SetActive(false);
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
            EndingVideoPlay();
            
            

        }
        
        private async void EndingVideoPlay()
        {
            vh.StopVideo();
            await vh.PrepareVideo(EndingVideoScreen, VideoHandler.VideoType.Ending);
            vh.PlayVideo();
            _videoPlayer.GetComponent<VideoPlayer>().isLooping = false;
            EndingUICanvas.SetActive(true);
            await UniTask.WaitUntil(() => !_videoPlayer.GetComponent<VideoPlayer>().isPlaying);

            Fade(2f).Forget();
            
            BGMManger.Instance.SoundStop();

        }

        async UniTask Fade(float duraion)
        {
            float time = 0;
            FadeScreen.gameObject.SetActive(true);
            while (time <= duraion)
            {
                time += Time.deltaTime;
                FadeScreen.alpha += time / duraion;
                await UniTask.Yield();
            }
        }
    }
}