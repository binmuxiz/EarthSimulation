using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameUIManager : MonoBehaviour
{
    public Canvas storyCanvas;
    public Canvas choiceCanvas;
    
    public Transform scoreParent;
    public TMP_Text[] scoreTexts;
    
    private Data.Score _score;

    public TMP_Text storyText;
    public TMP_Text timerText;

    public TMP_Text[] voteTexts; // 투표 개수 보이는 텍스트 
    public Button[] choices; 
    
    public bool[] isSelected; // N개의 선택지 중 하나만 선택할 수 있도록 

    private bool clickNextBtn;
    
    private bool isChoosed;
    private int myChoice = -1;

    private const int Timer = 10;
    private const int FinalRound = 5;
    
    public static bool storyPermitted = false;
    
    private void Awake()
    {
        storyCanvas.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        
        InitializeScore();
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable()");
        SharedData.OnVoted += RefreshUI;
    }

    // private void OnDisable()
    // {
    //     SharedData.Instance.OnVoted -= RefreshUI;
    // }

    private void RefreshUI(Dictionary<int, int> votes)
    {
        for (var i = 0; i < votes.Count; ++i)
        {
            voteTexts[i].text = $"{votes[i]}";
        }
    }
    
    private async UniTaskVoid Start()
    {
        Debug.Log("GameUIManager.Start()");
        await UniTask.WaitUntil(() => storyPermitted);
        
        Debug.Log("storyPermitted => " + storyPermitted);
        await Process();
    }

    private async UniTask Process()
    {
        await StoryProcess();
        await EndingProcess();
    }

    private async UniTask StoryProcess()
    {
        for (int i = 1; i <= FinalRound; i++)
        {
            Debug.Log($"Round {i}");
            if (i != 1) await RequestDataProcess();
            else        await RequestDataProcess(true);
            
            string[] storySentences = NetworkManager.GetData.story.Split(".");

            if (storySentences == null)
            {
                throw new NullReferenceException("GetData is Null");
            }
            
            for (int j = 0; j < storySentences.Length - 1; j++)
            {
                storySentences[j] += '.';
            }
            
            await ShowStory(storySentences);
            await ShowChoices(NetworkManager.GetData.choices);
            
// TODO 내 선택 말고 다수결 선택 받은 선택지 인덱스로 점수 저장 
            
            SetScore(NetworkManager.GetData.choices[NetworkManager.SendData.choice_index].score);
            
            if (NetworkManager.GetData.round == FinalRound) break;
            
            NetworkManager.GetData = null;
        }
    }

    private async UniTask EndingProcess()
    {
        
    }

    
    // 데이터 요청하고 응답받는 프로세스 
    private async UniTask RequestDataProcess(bool isFirstStory = false)
    {
        // 마스터 클라이언트가 모델 서버에 데이터 요청 후 다른 클라이언트에게 데이터 전달 
        if (RunnerController.Runner.IsSharedModeMasterClient)
        {
            Debug.Log("ReceiveDataProcess() => RequestStartData");

            string data = null;
            if (isFirstStory) data = await NetworkManager.RequestStartData();
            else              data = await NetworkManager.RequestData();

            if (string.IsNullOrEmpty(data))
            {
                Debug.Log("Data is Null Or Empty");
                return;
            }
            
            Debug.Log("----------- 응답 받은 데이터 ----------");
            Debug.Log(data);
            SharedData.Instance.SendJsonDataToPlayer(data);
        }
        else
        {
            // 다른 클라이언트는 데이터 대기
            await UniTask.WaitUntil(() => NetworkManager.GetData != null);
            Debug.Log(NetworkManager.GetData);
        }
    }
    
    // 스토리창 
    async UniTask ShowStory(string[] storySentences)
    {
        storyCanvas.gameObject.SetActive(true);
        clickNextBtn = false;

        for (int i = 0; i < storySentences.Length - 1; i++)
        {
            storyText.text = storySentences[i];
            
            await UniTask.WaitUntil(() => clickNextBtn);
            clickNextBtn = false;
        }
        
        SharedData.Instance.RpcReadDone();
        // 다른 클라이언트가 스토리를 다 읽을 때까지 대기 
        await UniTask.WaitUntil(() => RunnerController.Runner.SessionInfo.PlayerCount <= SharedData.ReadCount);

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        
        Debug.Log("All Read");
        SharedData.Instance.RpcClearReadCount();
        
        storyCanvas.gameObject.SetActive(false);
    }

    
    // 선택창
    async UniTask ShowChoices(List<Choice> choiceList)
    {
        Debug.Log("ShowChoices()");

        choiceCanvas.gameObject.SetActive(true);
        
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponentInChildren<TMP_Text>().text = choiceList[i].text;
            voteTexts[i].text = "0";
        }

        //SharedData.Instance.SetTimer(11f);
        
        for (int t = Timer; t >= 0; t--)
        {
            timerText.text = $"남은 시간 : {t}";
            //Debug.Log(SharedData.Instance.timer.IsRunning);
            //Debug.Log(SharedData.Ok);
            Debug.Log("IsRunning : " + SharedData.Instance.timer.IsRunning);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
        
        // 아무 선택이 없었을 때 랜덤 선택 
        if (!isChoosed) Choice(Random.Range(0, choices.Length));
        //await UniTask.WaitUntil(() => SharedData.Ok);
        
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        
        choiceCanvas.gameObject.SetActive(false);
        isChoosed = false;
        myChoice = -1;

        int maxChoiceIndex = 0;
        
        // todo 가장 많은 투표를 받은 선택지 인덱스 SendData에 저장
        if (RunnerController.Runner.IsSharedModeMasterClient)
        {
            Dictionary<int, int> votes = SharedData.Votes;
            
            // case 1111
            if (votes[0] == 1 && votes[1] == 1 && votes[2] == 1 && votes[3] == 1)
            {
                maxChoiceIndex = Random.Range(0, 4);
            }
            else
            {
                // votes 내림차순 정렬
                var sortedVotes = votes.OrderByDescending(x => x.Value);

                // case 2200
                var first = sortedVotes.First();
                var second = sortedVotes.ElementAt(1);

                if (first.Value == 2 && second.Value == 2)
                { 
                    int rand = Random.Range(0, 2);
                    
                    if (rand == 0) maxChoiceIndex = first.Key;
                    else maxChoiceIndex = second.Key;
                }
                else
                {
                    maxChoiceIndex = first.Key;
                }
            }
            NetworkManager.SendData.choice_index = maxChoiceIndex;
        }
        Debug.Log("maxChoiceIndex : " + maxChoiceIndex);
        SharedData.ClearVotes();
    }

    public void Choice(int idx)
    {
        if (myChoice == idx) return; // 동일 선택지 선택시 return
        
        SharedData.Instance.RpcVote(idx);
        if (isChoosed) SharedData.Instance.RpcVoteCancel(myChoice);    
        
        myChoice = idx;
        isChoosed = true;
    }

    private void SetScore(Score score)
    {
        _score.Environment += score.envScore;
        _score.Society += score.societyScore;
        _score.Technology += score.techScore;
        _score.Economy += score.economyScore;
        
        scoreTexts[0].text = _score.Environment.ToString();
        scoreTexts[1].text = _score.Society.ToString();
        scoreTexts[2].text = _score.Technology.ToString();
        scoreTexts[3].text = _score.Economy.ToString();       
    }

    public void NextButton()
    {
        clickNextBtn = true;   
    }
    
    private void InitializeScore()
    {
        _score = new Data.Score();
        
        // score text 초기화
        scoreTexts[0].text = _score.Environment.ToString();
        scoreTexts[1].text = _score.Society.ToString();
        scoreTexts[2].text = _score.Technology.ToString();
        scoreTexts[3].text = _score.Economy.ToString();        
    }
}
