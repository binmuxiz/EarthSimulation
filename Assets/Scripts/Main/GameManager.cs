#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using Global;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // UnityEvent를 사용하여 LoadingCanvas 활성화 제어
    public UnityEvent onGameStart;
    
    public void StartGame()
    {
        Debug.Log("GameManager.StartGame()");
        onGameStart.Invoke();
        // todo 원래는 바로 게임 씬으로 안넘어감 
        StartCoroutine(LoadGameScene());
    }
    
    private IEnumerator LoadGameScene()
    {
        Debug.Log("GameManager.LoadGameScene()");
        
        // todo 모든 인원이 찰 떄까지 대기
        yield return new WaitForSeconds(5);
        SceneManager.Instance.LoadScene("Game Scene Temp");        
    }
    
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
