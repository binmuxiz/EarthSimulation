#if UNITY_EDITOR
using UnityEditor;

#endif

using System;
using System.Collections;
using Global;
using Fusion;
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
        
        
        
        StartCoroutine(LoadGameStoryScene());
    }
    
    public void EnterGame(string RoomName = null)
    {
        Debug.Log("GameManager.StartGame()");

        onGameStart.Invoke();
        // todo 원래는 바로 게임 씬으로 안넘어감 
        
        
        FusionManager.Instance.StartGame(RoomName);
        //StartCoroutine(LoadGameStoryScene());
    }
    
    
    
    private IEnumerator LoadGameStoryScene()
    {
        Debug.Log("GameManager.LoadGameScene()");
        
        // todo 모든 인원이 찰 떄까지 대기
        yield return new WaitForSeconds(3);
        SceneManager.Instance.LoadScene("Game Scene");        
    }
    
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
