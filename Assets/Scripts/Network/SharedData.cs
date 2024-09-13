using System;
using System.Collections;
using System.Threading;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SharedData : NetworkBehaviour
{
    public static SharedData Instance;

    public static int CountReadStoryDone { get; set; }
    static int CountReady { get; set; }

    public static int MaxCount { get; set; } =2;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void CheckReadyRpc()
    {
        CountReady++;
        Debug.Log(CountReady);

       
        if (CountReady >= 2)
        {
            Debug.Log("ok");
            if (RunnerController.Runner.IsSceneAuthority)
            {
                RunnerController.Runner.LoadScene(SceneRef.FromIndex(4));
            }
            
           
        }
        
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void CheckReadStoryDoneRpc()
    {
        CountReadStoryDone++;
        Debug.Log(CountReadStoryDone);
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void ClearReadCountRpc()
    {
        CountReadStoryDone = 0;
        Debug.Log(CountReadStoryDone);
    }

    

    
    
   
    
    
    
    
    
    
}
