using Data;
using Fusion;
using UnityEngine;
using Newtonsoft.Json;

public class SharedData : NetworkBehaviour
{
    public static SharedData Instance;
    
    // 대기
    public static int ReadyCount { get; set; }
    public static int MaxCount { get; set; } = 2;
    
    // 스토리 읽은 플레이어 수
    public static int ReadStoryCount { get; set; }
    
    // 스토리 데이터
    public static string RealJson;
    
    // 직업

    [SerializeField]
    private Role role;
    
    public Role Role { get; set; }    
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void CheckReadyRpc() // 대기 - ready 버튼 
    {
        ReadyCount++;
        Debug.Log(ReadyCount);

        if (ReadyCount >= 2)
        {
            Debug.Log("ok");
            if (RunnerController.Runner.IsSceneAuthority)
            {
                RunnerController.Runner.LoadScene(SceneRef.FromIndex(3));
            }
        }
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
        UIManager.storyPermitted = true;
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

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void AssignJobRPC(Role role)
    {
        Role = role;
        Debug.Log(Instance + "=> Role : " + role);
    }
}
