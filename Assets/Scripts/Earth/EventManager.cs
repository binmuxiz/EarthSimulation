using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Data;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject primalObject;
    [SerializeField] private GameObject[] planetObjects;  // 각 이벤트에 대응하는 지구 오브젝트 배열
    
    [SerializeField] private CanvasGroup eventUICanvasGroup;
    [SerializeField] private Sprite[] eventUISprites;  // 각 이벤트에 대응하는 UI 오브젝트 배열
    
    private Image _eventUIImage;

    private int _currentIdx = -1;  // 현재 발생한 이벤트 인덱스

    // 각 이벤트의 조건을 관리하는 델리게이트 배열
    private Func<bool>[] _eventConditions;

    private List<int> used = new List<int>();
    
    private void Awake()
    {
        _eventUIImage = eventUICanvasGroup.gameObject.GetComponentInChildren<Image>();
        
        // 이벤트 조건 배열 설정
        _eventConditions = new Func<bool>[]
        {
            () => Score.Environment >= 18,   // 지속 가능
            () => Score.Technology >= 18,    // 첨단 산업 발전   
            () => Score.Society >= 18         // 사회 복지 시스템 확장
        };
    }

    void Start()
    {
        eventUICanvasGroup.alpha = 0;
        eventUICanvasGroup.interactable = eventUICanvasGroup.blocksRaycasts = false;

        // 모든 오브젝트 비활성화
        foreach (GameObject obj in planetObjects)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    public async UniTask Event()
    {
        for (int i = 0; i < _eventConditions.Length; i++)
        {
            if (used.Contains(i))
                continue;
            
            if (_eventConditions[i]())  // 조건이 만족되면
            {
                if (primalObject.activeSelf) primalObject.SetActive(false);
                Debug.Log($"이벤트 만족한 인덱스 : {i}");
                await TriggerEvent(i);  // 해당 이벤트 발생
                used.Add(i);
                break;  // 한번 이벤트가 발생하면 반복문 종료
            }
        }
    }

    private async UniTask TriggerEvent(int idx)
    {
        // 같은 이벤트가 중복 발생하지 않도록 확인
        if (_currentIdx == idx)
        {
            Debug.Log($"{idx}번 이미 띄우고 있음");
            return;
        }

        int previous = _currentIdx;
        _currentIdx = idx;

        if (previous == -1)
        {
            primalObject.SetActive(false);
        }
        else
        {
            // 이전에 활성화된 지구 오브젝트와 UI를 비활성화
            if (planetObjects[previous] != null)
            {
                planetObjects[previous].SetActive(false);
            }     
        }
        
        // UI 알림을 8초 동안 띄운 후 자동으로 끔
        await ShowEventUI(idx);

        // 새로운 지구 오브젝트와 UI를 활성화
        if (planetObjects[_currentIdx] != null)
        {
            planetObjects[_currentIdx].SetActive(true);
        }
    }

    private async UniTask ShowEventUI(int idx)
    {
        if (eventUISprites[idx] != null)
        {
            _eventUIImage.sprite = eventUISprites[idx];
            
            eventUICanvasGroup.alpha = 1;
            await UniTask.WaitForSeconds(5f);
            eventUICanvasGroup.alpha = 0;            
        }
    }
}