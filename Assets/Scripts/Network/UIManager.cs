using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
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
    public TMP_Text storyText;
    public TMP_Text timer;
    
    public UIState _UIState;
    
    private bool isClick;
    private bool isCoroutine;
    private bool isChoosing;
    private string[] storySentences;
    
    
    private IEnumerator Start()
    {
        _UIState = UIState.Idle;
        storyCanvas.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        yield return NetworkManager.instance.StartSendDataProcess();

        for (int i = 0; i < 5; i++)
        {
            storySentences = NetworkManager.instance._getData.text.Split("\n");

            yield return PresentingStory(storySentences);
            
            isChoosing = true;
            
            yield return ShowChoices(NetworkManager.instance._getData.choices);
            
            yield return NetworkManager.instance.SendDataProcess();
        }
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
        }
    }

    IEnumerator PresentingStory(string[] storySentences)
    {
        _UIState = UIState.Story;
        
        storyCanvas.gameObject.SetActive(true);
        isClick = false;
        for (int i = 0; i < storySentences.Length; i++)
        {
            storyText.text = storySentences[i];
            
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
        }

        for (int i = 10; i >= 0; i--)
        {
            if (!isChoosing) yield break; //enum
            
            timer.text = $"남은 시간 : {i}";
            yield return new WaitForSeconds(1f);
            if (i == 0)
            {
                int temp = Random.Range(1, 4);

                switch (temp)
                {
                    case 1:
                        Choice1();
                        break;
                    case 2:
                        Choice2();
                        break;
                    case 3:
                        Choice3();
                        break;
                }
            }
        }

        
    }

    public void Choice1()
    {
        NetworkManager.instance._sendData.story_id = NetworkManager.instance._getData.id;
        NetworkManager.instance._sendData.choice_index = 1;
        isChoosing = false; // enum
        choiceCanvas.gameObject.SetActive(false);
        //StartCoroutine(NetworkManager.instance.SendDataProcess());
    }
    
    public void Choice2()
    {
        NetworkManager.instance._sendData.story_id = NetworkManager.instance._getData.id;
        NetworkManager.instance._sendData.choice_index = 2;
        isChoosing = false; //enum
        choiceCanvas.gameObject.SetActive(false);
        //StartCoroutine(NetworkManager.instance.SendDataProcess());

    }
    public void Choice3()
    { 
        NetworkManager.instance._sendData.story_id = NetworkManager.instance._getData.id;
        NetworkManager.instance._sendData.choice_index = 3;
        isChoosing = false; //enum
        choiceCanvas.gameObject.SetActive(false);
        //StartCoroutine(NetworkManager.instance.SendDataProcess());
        

    }
    
}
