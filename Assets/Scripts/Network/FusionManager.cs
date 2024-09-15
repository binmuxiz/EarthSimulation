using Fusion;
using UnityEngine;

public class FusionManager : MonoBehaviour
{
    public static FusionManager Instance;
    
    public GameObject runnerPrefab;

    public static NetworkRunner Runner;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    
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
        });
    }
}

   
