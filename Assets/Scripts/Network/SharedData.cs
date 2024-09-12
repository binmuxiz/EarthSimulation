using System;
using System.Collections;
using System.Threading;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SharedData : NetworkBehaviour
{
    
    public static SharedData Instance;
    
    
    static int CountReady { get; set; }

    private int MaxCount { get; set; } =2;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void CheckReadyRpc()
    {
        CountReady++;
        Debug.Log(CountReady);

        Debug.Log(RunnerController.Runner.LocalPlayer.IsMasterClient);
        if (CountReady >= 2)
        {
            
            RunnerController.Runner.LoadScene(SceneRef.FromIndex(4));
            Debug.Log("ok");
            
           
        }
        
    }

    
    
   
    
    
    
    
    
    
}
