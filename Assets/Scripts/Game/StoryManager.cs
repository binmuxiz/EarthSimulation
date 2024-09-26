using System;
using Cysharp.Threading.Tasks;
using Fusion;
using Global;
using Handler;
using Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class StoryManager : Singleton<StoryManager>
    {
/*
 * ---------------- Fields -----------------
 */
        public VideoHandler mVideoHandler;
        public RawImage mScreen;

        public bool processPermitted = false;
        
        public Canvas storyCanvas;
        public Canvas choiceCanvas;
        public TMP_Text storyText;
        public TMP_Text[] choices;
        public TMP_Text roundText;
        
        private bool _clickNextBtn;
        private int _currentRound;
        
        private const int FinalRound = 3;

        private EventManager _eventManager;
        
/*
 * ----------------- Methods -----------------
 */
        private void Awake()
        {
            mVideoHandler = GameObject.Find("Video Player").GetComponent<VideoHandler>();
            _eventManager = GetComponent<EventManager>();
            storyCanvas.gameObject.SetActive(false);
            choiceCanvas.gameObject.SetActive(false);
        }

        private async void Start()
        {
            await UniTask.WaitUntil(() => processPermitted); // 스토리 시작 대기
            _currentRound = 0;
            await mVideoHandler.PrepareVideo(mScreen,VideoHandler.VideoType.Loading);

            for (int i = 1; i <= FinalRound; i++)
            {
                _currentRound = i;
                roundText.text = $"Round {i}";
                await Process();
            }
            
            mVideoHandler.StopVideo();
            EndingManager.Instance.processPermitted = true; // 엔딩 시작 
        }


        private async UniTask Process()
        {
            SharedData.ClearVotes();
            ActiveStoryUI(true);  
            mScreen.gameObject.SetActive(true);
            mVideoHandler.PlayVideo(); // 로딩 영상 재생 
            
            await RequestData();

            mScreen.gameObject.SetActive(false);
            mVideoHandler.PauseVideo();                             // 로딩 영상 일시정지 
            
            await ShowStory();  // 스토리  UI

            ActiveStoryUI(false);
            ActiveChoiceUI(true);
            
            await ShowChoices(); // 선택지 UI
            
            await VoteManager.Instance.VoteProcess();
            Debug.Log($"Selected Num => {SharedData.SelectedNum}");
            
            //바로 넘어가길래 잠깐 기다림
            await UniTask.Delay(1000);

            Score score = NetworkManager.Instance.GetScore(SharedData.SelectedNum);
            ScoreManager.Instance.SetScore(score);
            
            _eventManager.Event();
                
            if (_currentRound == FinalRound) return;
        
            NetworkManager.Instance.GetData = null;

            ActiveChoiceUI(false);
        }
        
        
        private async UniTask RequestData()
        {
            NetworkManager.Instance.GetData = null;
            
            if (RunnerController.Runner.IsSharedModeMasterClient) // 마스터 클라이언트인 경우 
            {
                string data;
                if (_currentRound == 1) 
                    data = await NetworkManager.Instance.RequestStartData();
                else              
                    data = await NetworkManager.Instance.RequestData();

                SharedData.Instance.SendJsonDataToPlayer(data); // 다른 클라이언트에게 데이터 전달 
            }
            else
            {
                await UniTask.WaitUntil(() => NetworkManager.Instance.GetData != null);
            }
            Debug.Log(NetworkManager.Instance.GetData);
        }


        
        // 스토리창 
        async UniTask ShowStory()
        {
            SharedData.Instance.RpcClearReadCount(); // ReadCount 초기화 
            string[] texts = NetworkManager.Instance.GetStory();

            _clickNextBtn = false;

            for (int i = 0; i < texts.Length - 1; i++)
            {
                storyText.text = texts[i];
            
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
        async UniTask ShowChoices()
        {
            string[] texts = NetworkManager.Instance.GetChoices();
            
            Debug.Log("ShowChoices()");
            
            for (int i = 0; i < texts.Length; i++)
            {
                choices[i].text = texts[i];
                // VoteManager.Instance.voteTexts[i].text = "0";  
            }
        }
        
            

        public void NextButton() // OnClicked
        {
            EffectSoundManager.Instance.ButtonEffect();
            _clickNextBtn = true;   
        }
                
        private void ActiveStoryUI(bool active)
        {
            if (active)
            {
                storyText.text = null; // text 초기화 
            }
            storyCanvas.gameObject.SetActive(active);    
        }
        
        private void ActiveChoiceUI(bool active)
        {
            choiceCanvas.gameObject.SetActive(active);    
        }

    }
}