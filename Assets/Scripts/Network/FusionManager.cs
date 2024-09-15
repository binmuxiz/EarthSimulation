using Fusion;
using Global;
using UnityEngine;

public class FusionManager : Singleton<FusionManager>
{
    public GameObject runnerPrefab;

    private static NetworkRunner Runner;
    
    public void StartGame(string RoomName = null)
    {
        string temp = RoomName;
        
        if (string.IsNullOrEmpty(temp)) temp = "777";
        if (Runner == null)
        {
            GameObject go = Instantiate(runnerPrefab);
            Runner = go.GetComponent<NetworkRunner>();
            Debug.Log(Runner);
            
        }
        
        Runner.StartGame(new StartGameArgs()
        {
            SessionName = temp,
            GameMode = GameMode.Shared,
            Scene = SceneRef.FromIndex(3),
            PlayerCount = 4
        });
    }
}

   
