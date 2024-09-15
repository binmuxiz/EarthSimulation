using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public GameObject runnerPrefab;

    public static NetworkRunner Runner;
    
    public void StartGame(string RoomName = null)
    {
        string temp = RoomName;
        
        if (string.IsNullOrEmpty(temp)) temp = "777";
        if (Runner == null)
        {
            GameObject go = Instantiate(runnerPrefab);
            Runner = go.GetComponent<NetworkRunner>();
        }

        Debug.Log(temp);
        Debug.Log("Runner : " + Runner);
       

        Runner.StartGame(new StartGameArgs()
        {
            SessionName = temp,
            GameMode = GameMode.Shared,
            Scene = SceneRef.FromIndex(1),
        });
    }

}
