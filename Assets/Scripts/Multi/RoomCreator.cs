using System.Collections.Generic;
using Fusion;
using Global;
using Home;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Multi
{
    public class RoomCreator : Singleton<RoomCreator>
    {
        public GameObject runnerPrefab;
        
        private static NetworkRunner Runner;
        
        public void CreateRoom(string roomName, int storyType, string sceneName)
        {
            if (Runner == null)
            {
                var go = Instantiate(runnerPrefab);
                Runner = go.GetComponent<NetworkRunner>();
            }

            var customProps = new Dictionary<string, SessionProperty>
            {
                ["story"] = storyType
            };

            Runner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Shared,
                Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath(sceneName)),
                SessionName = roomName,
                PlayerCount = 4,
                SessionProperties = customProps,
            });
        }
    }
}