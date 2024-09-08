using UnityEngine;

public class ToggleMode : MonoBehaviour
{
    public GameObject gameUI;  // 1번 오브젝트
    public GameObject earth;  // 2번 오브젝트

    void Start()
    {
        // 기본 상태: 1번 켜져 있고, 2번 꺼져 있음
        gameUI.SetActive(true);
        earth.SetActive(false);
    }

    // 1번 오브젝트에서 지구 버튼을 누르면 호출될 함수
    public void EarthButton()
    {
        gameUI.SetActive(false);  // 1번 오브젝트 꺼짐
        earth.SetActive(true);   // 2번 오브젝트 켜짐
    }

    // 2번 오브젝트에서 뒤로가기 버튼을 누르면 호출될 함수
    public void BackButton()
    {
        gameUI.SetActive(true);   // 1번 오브젝트 켜짐
        earth.SetActive(false);  // 2번 오브젝트 꺼짐
    }
}

