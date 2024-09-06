#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // UnityEvent를 사용하여 LoadingCanvas 활성화 제어
    public UnityEvent onGameStart;
    
    public void StartGame()
    {
        Debug.Log("StartGame()");
        // Loading Cavas 켜기
        // onGameStart.Invoke();
        
        // 일정 시간 대기
        
        
        
        // Game Scene Temp 로드
    }
    
    // private IEnumerator LoadGameScene()
    // {
    //     
    // }
    
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
