using System;
using System.Collections;
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

    private int _currentIdx;  // 현재 발생한 이벤트 인덱스

    // 각 이벤트의 조건을 관리하는 델리게이트 배열
    private Func<bool>[] _eventConditions;

    private void Awake()
    {
        _eventUIImage = eventUICanvasGroup.gameObject.GetComponentInChildren<Image>();
        
        // 이벤트 조건 배열 설정
        _eventConditions = new Func<bool>[]
        {
            () => Score.Environment < 10,                                                      // 환경 파괴
            () => Score.Environment >= 10 && Score.Technology >= 25 && Score.Economy >= 25,  // 첨단 산업 발전
            () => Score.Environment >= 20 && Score.Technology >= 20,                          // 지속 가능한 발전을 위한 한 걸음 
            () => Score.Society >= 25 && Score.Technology >= 15 && Score.Economy >= 15,      // 사회 복지 시스템 확장
        };
    }

    void Start()
    {
        eventUICanvasGroup.alpha = 0;
        eventUICanvasGroup.interactable = eventUICanvasGroup.blocksRaycasts = false;

        // 모든 오브젝트 비활성화
        foreach (GameObject obj in planetObjects)
        {
            obj.SetActive(false);
        }
    }

    public void Event()
    {
        for (int i = 0; i < _eventConditions.Length; i++)
        {
            if (_eventConditions[i]())  // 조건이 만족되면
            {
                if (primalObject.activeSelf) primalObject.SetActive(false);
                Debug.Log($"이벤트 만족한 인덱스 : {i}");
                TriggerEvent(i);  // 해당 이벤트 발생
                break;  // 한번 이벤트가 발생하면 반복문 종료
            }
        }
    }

    void TriggerEvent(int idx)
    {
        // 같은 이벤트가 중복 발생하지 않도록 확인
        if (_currentIdx == idx)
        {
            Debug.Log($"{idx}번 이미 띄우고 있음");
            return;
        }

        int previous = _currentIdx;
        _currentIdx = idx;
        
        // 이전에 활성화된 지구 오브젝트와 UI를 비활성화
        if (planetObjects[previous] != null)
        {
            planetObjects[previous].SetActive(false);
        }
        
        // UI 알림을 8초 동안 띄운 후 자동으로 끔
        StartCoroutine(ShowEventUI(idx));

        // 새로운 지구 오브젝트와 UI를 활성화
        if (planetObjects[_currentIdx] != null)
        {
            planetObjects[_currentIdx].SetActive(true);
        }
    }

    IEnumerator ShowEventUI(int idx)
    {
        if (eventUISprites[idx] != null)
        {
            _eventUIImage.sprite = eventUISprites[idx];
            
            eventUICanvasGroup.alpha = 1;            
            yield return new WaitForSeconds(8f);
            eventUICanvasGroup.alpha = 0;            
        }
    }
}