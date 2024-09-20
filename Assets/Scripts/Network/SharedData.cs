using Fusion;
using UnityEngine;
using Newtonsoft.Json;

public class SharedData : NetworkBehaviour
{
    public static SharedData Instance;

    
    public static int CountReadStoryDone { get; set; }
    static int CountReady { get; set; }
    
    public static int MaxCount { get; set; } = 2;

    public static string RealJson;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void CheckReadyRpc() // 대기 - ready 버튼 
    {
        CountReady++;
        Debug.Log(CountReady);


        if (CountReady >= 2)
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
        CountReadStoryDone++;
        Debug.Log(CountReadStoryDone);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void ClearReadCountRpc() // 
    {
        CountReadStoryDone = 0;
        UIManager.storyPermitted = true;
        Debug.Log(CountReadStoryDone);
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

}
