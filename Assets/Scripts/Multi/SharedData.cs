using Data;
using Fusion;
using UnityEngine;
using Newtonsoft.Json;

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
    public static int ReadIntroCount { get; set; }
    
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

    // 인트로 다 봤는지
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcReadIntro() 
    {
        ReadIntroCount++;
        Debug.Log($"ReadIntroCount => {ReadIntroCount}");
    }

    // 인트로 읽은 수 초기화 
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcClearReadCount() 
    {
        ReadIntroCount = 0;
        GameUIManager.storyPermitted = true;
        Debug.Log($"ReadIntroCount => {ReadIntroCount}");
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
    public void AssignJobRPC(RoleType role)
    {
        Role = role;
        this.role = role; 
    }
}
