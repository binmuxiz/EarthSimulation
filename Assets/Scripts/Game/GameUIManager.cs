using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameUIManager : MonoBehaviour
{
    public Canvas storyCanvas;
    public Canvas choiceCanvas;
    
    public Transform scoreParent;
    private TMP_Text[] scoreTexts;
    
    private Data.Score _score;

    public TMP_Text storyText;
    public TMP_Text timerText;
    
    public Button[] choices;
    public bool[] _isSelected; // N개의 선택지 중 하나만 선택할 수 있도록 
    public GameObject[] votes;
    public GameObject checkmarkPrefab;

    private bool clickNextBtn;
    
    private bool isChoosed;
    private int _choiceNum = -1;

    private const int Timer = 10;
    private const int FinalRound = 5;
    
    public static bool storyPermitted = false;
    
    private void Awake()
    {
        storyCanvas.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);

        InitializeScore();
    }

    private async UniTaskVoid Start()
    {
        await UniTask.WaitUntil(() => storyPermitted);
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
            
            // todo show UI Process

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
            SetScore(NetworkManager.GetData.choices[_choiceNum - 1].score);
            
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
        storyCanvas.gameObject.SetActive(false);
    }

    
    // 선택창
    async UniTask ShowChoices(List<Choice> choiceList)
    {
        // _UIState = UIState.Choice;
        choiceCanvas.gameObject.SetActive(true);
        
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponentInChildren<TMP_Text>().text = choiceList[i].text;
            // votes[i].text = "0";
        }

        for (int t = Timer; t >= 0; t--)
        {
            timerText.text = $"남은 시간 : {t}";
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
        
        // 아무 선택이 없었을 때 랜덤 선택 
        if (!isChoosed) Choice(Random.Range(0, choices.Length));
        
        
        choiceCanvas.gameObject.SetActive(false);
        isChoosed = false;
        _choiceNum = -1;
    }

    public void Choice(int idx)
    {
        if (_choiceNum == idx) return;
        
        _choiceNum = idx;
        isChoosed = true;

        // if (_isSelected[_choiceNum])
        // {
        //     _isSelected[_choiceNum] = false;
        //     
        //     int voteNum = votes[_choiceNum].transform.childCount;
        //     if (voteNum > 0)
        //     {
        //         // checkmark 제거 
        //         Destroy(votes[_choiceNum].transform.GetChild(voteNum-1));
        //     }
        // }

        // NetworkManager._sendData.choice_index = _choiceNum;
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
        int scoreCnt = scoreParent.childCount;
        scoreTexts = new TMP_Text[scoreCnt];
        for (int i = 0; i < scoreCnt; i++)
        {
            scoreTexts[i] = scoreParent.GetChild(i).GetComponentInChildren<TMP_Text>();
        }

        scoreTexts[0].text = _score.Environment.ToString();
        scoreTexts[1].text = _score.Society.ToString();
        scoreTexts[2].text = _score.Technology.ToString();
        scoreTexts[3].text = _score.Economy.ToString();        
    }
}
