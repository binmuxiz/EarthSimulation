using Fusion;
using Global;
using UnityEngine;

public class FusionManager : Singleton<FusionManager>
{
    public GameObject runnerPrefab;

    private static NetworkRunner Runner;
    
    public void StartGame(string roomName = null)
    {
        
        if (string.IsNullOrEmpty(roomName)) roomName = "777";
        if (Runner == null)
        {
            GameObject go = Instantiate(runnerPrefab);
            Runner = go.GetComponent<NetworkRunner>();
            Debug.Log(Runner);
            
        }
        
        Runner.StartGame(new StartGameArgs()
        {
            SessionName = roomName,
            GameMode = GameMode.Shared,
            Scene = SceneRef.FromIndex(3),
            PlayerCount = 4
        });
    }
}