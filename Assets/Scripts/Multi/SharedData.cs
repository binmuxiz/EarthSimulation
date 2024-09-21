using Data;
using Fusion;
using UnityEngine;
using Newtonsoft.Json;

public class SharedData : NetworkBehaviour
{
    public static SharedData Instance;
    
    // 역할 
    [SerializeField]
    private Role role;
    public Role Role { get; set; }
    
    // 대기
    public static int ReadyCount { get; private set; }
    public static int MaxCount { get; set; } = 2;
    
    // 스토리 읽은 플레이어 수
    public static int ReadStoryCount { get; set; }
    
    // 스토리 데이터
    public static string RealJson;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
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

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void CheckReadStoryDoneRpc() // 스토리 다 읽었는지 
    {
        ReadStoryCount++;
        Debug.Log(ReadStoryCount);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void ClearReadCountRpc() // 
    {
        ReadStoryCount = 0;
        GameUIManager.storyPermitted = true;
        Debug.Log(ReadStoryCount);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void TransferJsonRpc(string Json) // 다른 클라이언트에게 스토리 데이터 전송 
    {
        RealJson = Json;
        Debug.Log(RealJson);
    }
    
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void ParsingRpc(string Json) 
    {
        NetworkManager._getData = JsonConvert.DeserializeObject<GetData>(Json);
        Debug.Log("Parsing Complette");
    }
    
    
    // 역할 배정 
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void AssignJobRPC(Role role)
    {
        Role = role;
    }
}
