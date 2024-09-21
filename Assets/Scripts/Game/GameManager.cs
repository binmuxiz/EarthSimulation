using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using GameStory;
using Global;
using Multi;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameIntroUIController gameIntroUIController;

    private void Start()
    {
  
        
        // AssignJob();
        
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
        if (!RunnerController.Runner.IsSharedModeMasterClient) return;

        Debug.Log("I'm Master client");

        Role[] roles = (Role[])Enum.GetValues(typeof(Role));

        foreach (var role in roles)
        {
            Debug.Log(role);
        }
        
        // List<SharedData> players =  SharedDataList.Instance.SharedDatas;
        //
        // for (int i = 0; i < players.Count; i++)
        // {
        //     players[i].AssignJobRPC(roles[i]);
        // }
    }
}
