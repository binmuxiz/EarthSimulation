using System;
using Cysharp.Threading.Tasks;
using Global;
using Network;
using TMPro;
using UnityEngine;

namespace Game
{
    public class StoryManager : Singleton<StoryManager>
    {
/*
 * ---------------- Fields -----------------
 */
        public bool processPermitted = false;
        
        public Canvas storyCanvas;
        public Canvas choiceCanvas;
        public TMP_Text storyText;
        public TMP_Text[] choices; 
        
        private bool _clickNextBtn;
        private int _currentRound;
        
        private const int FinalRound = 5;
        
/*
 * ----------------- Methods -----------------
 */
        private void Awake()
        {
            storyCanvas.gameObject.SetActive(false);
            choiceCanvas.gameObject.SetActive(false);
        }

        private async void Start()
        {
            await UniTask.WaitUntil(() => processPermitted); // 스토리 시작 대기
            _currentRound = 0;

            for (int i = 1; i <= FinalRound; i++)
            {
                _currentRound = i;
                Debug.Log($"Round {i}");
                await Process();
            }
                
            EndingManager.Instance.processPermitted = true; // 엔딩 시작 
        }


        private async UniTask Process()
        {
            SharedData.ClearVotes();
            
            await RequestDataProcess();

            ActiveStoryUI(true);
            
            await ShowStory(NetworkManager.Instance.GetStory());  // 스토리  UI

            ActiveStoryUI(false);
            ActiveChoiceUI(true);
            
            await ShowChoices(NetworkManager.Instance.GetChoices()); // 선택지 UI
            
            int maxChoice = await VoteManager.Instance.VoteProcess();
        
// TODO 내 선택 말고 다수결 선택 받은 선택지 인덱스로 점수 저장 
        
            ScoreManager.Instance.SetScore(NetworkManager.Instance.GetScore(maxChoice));
        
            if (_currentRound == FinalRound) return;
        
            NetworkManager.Instance.GetData = null;

            ActiveChoiceUI(false);
        }
        
        
        private async UniTask RequestDataProcess()
        {
            NetworkManager.Instance.GetData = null;
            
            if (RunnerController.Runner.IsSharedModeMasterClient) // 마스터 클라이언트인 경우 
            {
                string data = await RequestData(); // 데이터 요청 
                SharedData.Instance.SendJsonDataToPlayer(data); // 다른 클라이언트에게 데이터 전달 
            }
            else
            {
                await UniTask.WaitUntil(() => NetworkManager.Instance.GetData != null);
            }
            Debug.Log(NetworkManager.Instance.GetData);
        }


        
        // 스토리창 
        async UniTask ShowStory(string[] story)
        {
            SharedData.Instance.RpcClearReadCount(); // ReadCount 초기화 

            _clickNextBtn = false;

            for (int i = 0; i < story.Length - 1; i++)
            {
                storyText.text = story[i];
            
                await UniTask.WaitUntil(() => _clickNextBtn);
                _clickNextBtn = false;
            }

            MessageManager.Instance.ShowWaitOtherClientMessage();
        
            SharedData.Instance.RpcReadDone();      
            await UniTask.WaitUntil(() => RunnerController.Runner.SessionInfo.PlayerCount <= SharedData.ReadCount);
            Debug.Log("All Read");
          
            MessageManager.Instance.HideWaitOtherClientMessage();
        }


        // 선택창
        async UniTask ShowChoices(string[] texts)
        {
            Debug.Log("ShowChoices()");
            
            for (int i = 0; i < texts.Length; i++)
            {
                choices[i].text = texts[i];
                // VoteManager.Instance.voteTexts[i].text = "0";  
            }
        }
        
            
        // 데이터 요청하고 응답받는 프로세스 
        private async UniTask<string> RequestData()
        {
            if (_currentRound == 1) 
                return await NetworkManager.Instance.RequestStartData();
            else              
                return await NetworkManager.Instance.RequestData();
        }
            
        public void NextButton() // OnClicked
        {
            _clickNextBtn = true;   
        }
                
        private void ActiveStoryUI(bool active)
        {
            storyCanvas.gameObject.SetActive(active);    
        }
        
        private void ActiveChoiceUI(bool active)
        {
            choiceCanvas.gameObject.SetActive(active);    
        }

    }
}