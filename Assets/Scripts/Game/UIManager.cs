using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public Canvas storyCanvas;
    public Canvas choiceCanvas;
    
    public Transform scoreParent;
    private TMP_Text[] scoreTexts;
    
    private Data.Score _score;

    public TMP_Text storyText;
    public TMP_Text timerText;
    
    private string[] storySentences;

    public Button[] choices;
    public bool[] _isSelected; // N개의 선택지 중 하나만 선택할 수 있도록 
    public GameObject[] votes;
    public GameObject checkmarkPrefab;

    private bool clickNextBtn;
    
    private bool isChoosed;
    private int _choiceNum = -1;

    private const int timer = 10;
    private const int _finalRound = 5;
    public static bool storyPermitted = false;

    private void Awake()
    {
        InitializeScore();
    }
    

    private async UniTaskVoid Start()
    {
        storyCanvas.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);

        await UniTask.WaitUntil(() => storyPermitted);
        Debug.Log("tqtqtq");
        await ReceiveDataProcess();
    }

    private void Update()
    {
        Debug.Log(storyPermitted);
    }

    private async UniTask ReceiveDataProcess()
    {
        await NetworkManager.instance.StartSendDataProcess();
        var getData = NetworkManager.instance._getData;

        while (true)
        {
            Debug.Log(getData);

            storySentences = getData.story.Split(".");

            for (int j = 0; j < storySentences.Length - 1; j++)
            {
                storySentences[j] += '.';
            }

            await PresentingStory(storySentences);
            await ShowChoices(getData.choices);
            SetScore(getData.choices[_choiceNum - 1].score);

            if (getData.round == _finalRound) break;
            
            await NetworkManager.instance.SendDataProcess();
            getData = NetworkManager.instance._getData;
        }
        
        // TODO 엔딩 
    }

    private void StoryProcess()
    {
        
    }

    private void EndingProcess()
    {
        
    }

    async UniTask PresentingStory(string[] storySentences)
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

    async UniTask ShowChoices(List<Choice> choiceList)
    {
        // _UIState = UIState.Choice;
        choiceCanvas.gameObject.SetActive(true);
        
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponentInChildren<TMP_Text>().text = choiceList[i].text;
            // votes[i].text = "0";
        }

        for (int t = timer; t >= 0; t--)
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

    void Choice(int idx)
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

        NetworkManager.instance._sendData.choice_index = _choiceNum;
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
