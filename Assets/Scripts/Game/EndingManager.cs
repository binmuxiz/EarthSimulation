using System;
using Cysharp.Threading.Tasks;
using Global;
using Network;
using UnityEngine;

namespace Game
{
    public class EndingManager : Singleton<EndingManager>
    {
        public bool processPermitted = false;
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

        }
    }
}