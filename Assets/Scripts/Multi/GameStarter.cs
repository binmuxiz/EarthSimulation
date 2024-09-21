using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using Global;
using UnityEngine;

public class GameStarter : Singleton<GameStarter>
{
    public GameObject runnerPrefab;
    
    private NetworkRunner Runner;

    public async Task StartGame(string roomName, Home.GameStory gameStory)
    {
        if (Runner == null)
        {
            GameObject go = Instantiate(runnerPrefab);
            Runner = go.GetComponent<NetworkRunner>();
        }

        var customProps = new Dictionary<string, SessionProperty>
        {
            ["story"] = (int)gameStory
        };
        
        StartGameArgs args = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            Scene = SceneRef.FromIndex(2),
            PlayerCount = 4,
            SessionProperties = customProps,
        };

        if (!string.IsNullOrEmpty(roomName))
        {
            args.SessionName = roomName;
        }
        
        var result = await Runner.StartGame(args);

        if (result.Ok)
        {
            Debug.Log("Result.OK");
        }
        else
        {
            Debug.LogError($"Failed to Start: {result.ShutdownReason}");
        }

    }
}