using System;
using System.Collections.Generic;
using Data;
using Fusion;
using GameStory;
using Global;
using Multi;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameIntroUIController gameIntroUIController;

    public List<SharedData> sharedDatas;

    private void Start()
    {
        sharedDatas = PlayerManager.Instance.players;
        Destroy(PlayerManager.Instance.gameObject);
        
        AssignJob();
        
        // yield return gameIntroUIController.ShowIntro();
        
        /*
        SharedData.Instance.CheckReadStoryDoneRpc();
        yield return new WaitUntil(() => SharedData.MaxCount <= SharedData.ReadStoryCount);
        Debug.Log("모두 다 읽음");
        Debug.Log(UIManager.storyPermitted);
           
        SharedData.Instance.ClearReadCountRpc();
        */
    }
    
    private void AssignJob()
    {
        if (RunnerController.Runner.IsSharedModeMasterClient)
        {
            Debug.Log("Im master client");
            
            Role[] roles = (Role[])Enum.GetValues(typeof(Role));

            for (int i = 0; i < sharedDatas.Count; i++)
            {
                Debug.Log($"Role {i} : {roles[i]}");
                sharedDatas[i].AssignJobRPC(roles[i]);
            }    
        }
    }
}
