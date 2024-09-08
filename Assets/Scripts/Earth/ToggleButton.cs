using UnityEngine;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    public GameObject main;  // 1번 오브젝트
    public GameObject eath;  // 2번 오브젝트

    void Start()
    {
        // 기본 상태: 1번 켜져 있고, 2번 꺼져 있음
        main.SetActive(true);
        eath.SetActive(false);
    }

    // 1번 오브젝트에서 지구 버튼을 누르면 호출될 함수
    public void EarthButton()
    {
        main.SetActive(false);  // 1번 오브젝트 꺼짐
        eath.SetActive(true);   // 2번 오브젝트 켜짐
    }

    // 2번 오브젝트에서 뒤로가기 버튼을 누르면 호출될 함수
    public void BackButton()
    {
        main.SetActive(true);   // 1번 오브젝트 켜짐
        eath.SetActive(false);  // 2번 오브젝트 꺼짐
    }
}

