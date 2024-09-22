using System;
using Data;
using Fusion;
using Fusion.Sockets;
using Global;
using UnityEngine;

public class SharedData : NetworkBehaviour
{
    public static SharedData Instance;
    
    // 역할 
    [SerializeField]
    private RoleType role; // 프로퍼티와 연결되어 있지 않음 
    public RoleType Role { get; private set; }
    
    // Ready
    public static int ReadyCount { get; private set; }
    
    // 스토리 읽은 플레이어 수
    public static int ReadCount { get; set; }
    
    // 스토리 데이터
    public static string StoryData;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public override void Spawned()
    {
        if (!HasStateAuthority) return;

        Instance = this;

        ReadyCount = 0;
    }

    // ready 버튼  
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcReady() 
    {
        ReadyCount++;
        Debug.Log($"ReadyCount Changed : {ReadyCount}");
    }
    
        
    // 역할 배정 
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void AssignJobRPC(RoleType role)
    {
        Role = role;
        this.role = role; 
    }

    // 인트로 다 봤는지
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcReadDone() 
    {
        ReadCount++;
        Debug.Log($"ReadIntroCount => {ReadCount}");
    }

    // 인트로 읽은 수 초기화 
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcClearReadCount() 
    {
        ReadCount = 0;
        GameUIManager.storyPermitted = true;
        Debug.Log($"ReadIntroCount => {ReadCount}");
    }

    // 다른 클라이언트에게 스토리 데이터 전송
    public void SendJsonDataToPlayer(string data)
    {
        var bytes = TypeConverter.StringToByte(data);
        var key = ReliableKey.FromInts(11, 22, 0, 0);

        foreach (var playerRef in RunnerController.Runner.ActivePlayers)
        {
            if (playerRef != RunnerController.Runner.LocalPlayer)
            {
                RunnerController.Runner.SendReliableDataToPlayer(playerRef, key, bytes);     
            }
        }
    }
}
