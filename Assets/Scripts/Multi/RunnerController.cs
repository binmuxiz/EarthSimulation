using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Sockets;
using Multi;
using Unity.VisualScripting;
using UnityEngine;

public class RunnerController : SimulationBehaviour, INetworkRunnerCallbacks
{
    
    public static NetworkRunner Runner;
    public NetworkPrefabRef SharedDataPrefab;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Runner = GetComponent<NetworkRunner>();
        
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
       
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        int playerCnt = runner.ActivePlayers.Count();
        Debug.Log($"현재 세션에 있는 플레이어 수 : {playerCnt}");
        
        foreach (var p in runner.ActivePlayers)
        {
            Debug.Log($"playerRef : {player}");
        }
        
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(SharedDataPrefab, Vector3.zero, Quaternion.identity);
        }

        GameObject[] networkObjects = GameObject.FindGameObjectsWithTag("NetworkObject");

        foreach (var obj in networkObjects)
        {
            if (!SharedDataList.Instance.SharedDatas.Contains(obj.GetComponent<SharedData>()))
            {
                SharedDataList.Instance.AddSharedData(obj.GetComponent<SharedData>());
            }
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}
