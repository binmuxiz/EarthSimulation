using System.Collections;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject[] earthObjects;  // 각 이벤트에 대응하는 지구 오브젝트 배열
    public GameObject[] eventUIs;  // 각 이벤트에 대응하는 UI 오브젝트 배열
    public Data.Score scoreSystem;  // 점수 시스템

    private GameObject previousEarthObject;  // 이전에 활성화된 지구 오브젝트
    private GameObject previousUIObject;  // 이전에 활성화된 UI 오브젝트
    private int currentEventIndex = -1;  // 현재 발생한 이벤트 인덱스

    // 각 이벤트의 조건을 관리하는 델리게이트 배열
    private System.Func<bool>[] eventConditions;

    void Start()
    {
        // 모든 오브젝트 비활성화
        foreach (GameObject obj in earthObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject ui in eventUIs)
        {
            ui.SetActive(false);
        }

        // 이벤트 조건 배열 설정
        eventConditions = new System.Func<bool>[]
        {
            () => scoreSystem.Technology >= 25,  // 첨단 산업 발전 조건
            () => scoreSystem.Environment <= 10, // 대기 오염 조건
            () => scoreSystem.Environment <= 5,  // 산림 파괴 조건
            () => scoreSystem.Economy <= 10,     // 경제 붕괴 조건
            () => scoreSystem.Society >= 50,     // 사회 복지 시스템 확장 조건
        };
    }

    void Update()
    {
        // 반복문으로 조건 배열을 돌면서 조건이 만족되는지 확인
        for (int i = 0; i < eventConditions.Length; i++)
        {
            if (eventConditions[i]())  // 조건이 만족되면
            {
                TriggerEvent(i);  // 해당 이벤트 발생
                break;  // 한번 이벤트가 발생하면 반복문 종료
            }
        }
    }

    void TriggerEvent(int eventIndex)
    {
        // 같은 이벤트가 중복 발생하지 않도록 확인
        if (currentEventIndex == eventIndex) return;

        // 이전에 활성화된 지구 오브젝트와 UI를 비활성화
        if (previousEarthObject != null)
        {
            previousEarthObject.SetActive(false);
        }
        if (previousUIObject != null)
        {
            previousUIObject.SetActive(false);
        }

        // 새로운 지구 오브젝트와 UI를 활성화
        earthObjects[eventIndex].SetActive(true);
        eventUIs[eventIndex].SetActive(true);

        // 현재 오브젝트 추적
        previousEarthObject = earthObjects[eventIndex];
        previousUIObject = eventUIs[eventIndex];

        // 현재 이벤트 인덱스 업데이트
        currentEventIndex = eventIndex;

        // UI 알림을 8초 동안 띄운 후 자동으로 끔
        StartCoroutine(ShowEventUI(eventIndex));
    }

    IEnumerator ShowEventUI(int eventIndex)
    {
        eventUIs[eventIndex].SetActive(true);
        yield return new WaitForSeconds(8f);
        earthObjects[eventIndex].SetActive(false);
    }
}