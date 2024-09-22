using System.Collections.Generic;
using Fusion;
using Home;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

public class GameStarter : MonoBehaviour
{
    public enum GameStory: int
    {
        NovaTerra,
        EarthRestoration,
        EarthShift
    }
    
    private const string LoadingSceneName = "Loading Scene";
    
    public GameObject runnerPrefab;
    private static NetworkRunner Runner;

    public GameStory gameStory;
    
    [SerializeField] TMP_InputField nickName;
    [SerializeField] TMP_InputField roomName;


    void Awake()
    {
        nickName.text = "";
        roomName.text = "";
    }

    public void OnGameStart()
    {
        if (nickName.text.IsNullOrEmpty())
        {
            Debug.Log("닉네임을 입력하세요");
            return;
        }
            
        StartCoroutine(HomeUIManager.Instance.HideGameStartMenu());
         
        // 로딩 화면 
        HomeUIManager.Instance.ShowLoadingView();

        if (string.IsNullOrEmpty(roomName.text)) roomName.text = null;
        
        CreateRoom(roomName.text, gameStory);
    }

    private void CreateRoom(string roomName, GameStory gameStory)
    {
        if (Runner == null)
        {
            var go = Instantiate(runnerPrefab);
            Runner = go.GetComponent<NetworkRunner>();
        }

        var customProps = new Dictionary<string, SessionProperty>
        {
            ["story"] = (int)gameStory
        };

        Runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath(LoadingSceneName)),
            SessionName = roomName,
            PlayerCount = 4,
            SessionProperties = customProps,
        });
    }
}