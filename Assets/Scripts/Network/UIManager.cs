using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum UIState
{
    Idle,
    Story,
    Choice
}

public class UIManager : MonoBehaviour
{
    public Canvas storyCanvas;
    public Canvas choiceCanvas;
    
    public Button[] choices;
    public TMP_Text[] votes;
    
    public TMP_Text storyText;
    public TMP_Text timerText;
    
    public UIState _UIState;
    
    private bool isClick;
    private bool isCoroutine;
    private bool isChoosing;
    private string[] storySentences;

    private bool isFirstStory;
    private bool isChoosed;

    private int timer = 10;
    
    private IEnumerator Start()
    {
        _UIState = UIState.Idle;
        storyCanvas.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        
        yield return NetworkManager.instance.StartSendDataProcess();

        for (int i = 0; i < 5; i++)
        {
            if (i == 0) isFirstStory = true;
            else isFirstStory = false;
            storySentences = NetworkManager.instance._getData.text.Split("\n");

            Debug.Log("Story " + i);
            foreach (var story in storySentences)
            {
                Debug.Log(story);
            }
            
            yield return PresentingStory(storySentences);
            
            //isChoosing = true;
            
            yield return ShowChoices(NetworkManager.instance._getData.choices);
            
            yield return NetworkManager.instance.SendDataProcess();
        }
        
    }

    

    IEnumerator PresentingStory(string[] storySentences)
    {
        _UIState = UIState.Story;
        int startIndex;
        
        storyCanvas.gameObject.SetActive(true);
        isClick = false;

        if (isFirstStory) startIndex = 1;
        else startIndex = 0;
        
        for (int i = startIndex; i < storySentences.Length; i++)
        {
            storyText.text = storySentences[i] + "\n";
            
            yield return new WaitUntil(() => isClick);
            isClick = false;
            if (i == this.storySentences.Length - 1)
            {
                storyCanvas.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator ShowChoices(List<Choice> choices_list)
    {
        _UIState = UIState.Choice;
        choiceCanvas.gameObject.SetActive(true);
        
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponentInChildren<TMP_Text>().text = choices_list[i].text;
            votes[i].text = "0";
        }

        int t = timer;
        for (; t >= 0; t--)
        {
            if (_UIState != UIState.Choice ) yield break; //enum
            
            timerText.text = $"남은 시간 : {t}";
            yield return new WaitForSeconds(1f);
        }
        // 아무 선택이 없었을 때 랜덤 선택 
        if (t < 0 && !isChoosed)
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    Choice(1);
                    break;
                case 2:
                    Choice(2);
                    break;
                case 3:
                    Choice(3);
                    break;
            }
        }
        
        choiceCanvas.gameObject.SetActive(false);
        _UIState = UIState.Idle;
        isChoosed = false;
    }
    
    public void Choice(int idx)
    {
        votes[idx-1].text = OnePersonChoose(votes[idx-1].text);
            
        NetworkManager.instance._sendData.story_id = NetworkManager.instance._getData.round;
        NetworkManager.instance._sendData.choice_index = idx;
        Debug.Log(NetworkManager.instance._sendData.choice_index);
        
        isChoosed = true;
    }


    public void NextButton()
    {
        isClick = true;
    }


    string OnePersonChoose(string s)
    {
        int temp = Int32.Parse(s);
        temp++;
        return temp.ToString();
    }
    
}
