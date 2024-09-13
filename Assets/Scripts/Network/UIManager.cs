using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public Canvas storyCanvas;
    public Canvas choiceCanvas;
    
    public Button[] choices;
    public TMP_Text[] votes;

    public Transform scoreParent;
    private TMP_Text[] scoreTexts;
    private Data.Score _score;

    public TMP_Text storyText;
    public TMP_Text timerText;
    
    private string[] storySentences;

    private bool clickNextBtn;
    private bool isChoosed;
    private int _choiceNum;

    private const int timer = 10;
    private const int _finalRound = 5;


    private void Awake()
    {
        InitializeScore();
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

    private void Start()
    {
        storyCanvas.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);

        StartCoroutine(ReceiveDataProcess());
    }

    private IEnumerator ReceiveDataProcess()
    {
        yield return NetworkManager.instance.StartSendDataProcess();
        var getData = NetworkManager.instance._getData;

        while (true)
        {
            Debug.Log(getData);

            storySentences = getData.story.Split(".");

            for (int j = 0; j < storySentences.Length - 1; j++)
            {
                storySentences[j] += '.';
            }

            yield return PresentingStory(storySentences);
            yield return ShowChoices(getData.choices);
            SetScore(getData.choices[_choiceNum - 1].score);

            if (getData.round == _finalRound) break;
            
            yield return NetworkManager.instance.SendDataProcess();
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

    IEnumerator PresentingStory(string[] storySentences)
    {
        storyCanvas.gameObject.SetActive(true);
        clickNextBtn = false;

        for (int i = 0; i < storySentences.Length - 1; i++)
        {
            storyText.text = storySentences[i];
            
            yield return new WaitUntil(() => clickNextBtn);
            clickNextBtn = false;
        }
        storyCanvas.gameObject.SetActive(false);
    }

    IEnumerator ShowChoices(List<Choice> choiceList)
    {
        // _UIState = UIState.Choice;
        choiceCanvas.gameObject.SetActive(true);
        
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponentInChildren<TMP_Text>().text = choiceList[i].text;
            votes[i].text = "0";
        }

        for (int t = timer; t >= 0; t--)
        {
            timerText.text = $"남은 시간 : {t}";
            yield return new WaitForSeconds(1f);
        }
        
        // 아무 선택이 없었을 때 랜덤 선택 
        if (!isChoosed) Choice(Random.Range(1, choices.Length));

        choiceCanvas.gameObject.SetActive(false);
        isChoosed = false;
    }

    void Choice(int idx)
    {
        Debug.Log($"선택 번호 : {idx}");
        _choiceNum = idx;
        isChoosed = true;
        votes[idx - 1].text = (Int32.Parse(votes[idx - 1].text) + 1).ToString();
        NetworkManager.instance._sendData.choice_index = idx;
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
}
