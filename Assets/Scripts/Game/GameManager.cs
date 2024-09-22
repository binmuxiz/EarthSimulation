using System;
using System.Collections;
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

    public Dictionary<RoleType, Role> RoleDict = new();

    private IEnumerator Start()
    {
        sharedDatas = PlayerManager.Instance.players;
        Destroy(PlayerManager.Instance.gameObject);
        
        AssignJob(); // 직업 배정 

        yield return gameIntroUIController.ShowIntro();
        
        SharedData.Instance.RpcReadDone();
        
        // 다른 클라이언트가 인트로를 다 볼때까지 대기
        yield return new WaitUntil(() =>
        {   
            Debug.Log("SharedData.ReadCount : " + SharedData.ReadCount);
            return RunnerController.Runner.SessionInfo.PlayerCount <= SharedData.ReadCount;
        });
        
        SharedData.Instance.RpcClearReadCount();
        Debug.Log("ReadAll");

        
        GameUIManager.storyPermitted = true;
    }
    
    private void AssignJob()
    {
        if (RunnerController.Runner.IsSharedModeMasterClient)
        {
            Debug.Log("Im master client");
            
            RoleType[] roleTypes = (RoleType[])Enum.GetValues(typeof(RoleType));

            for (int i = 0; i < sharedDatas.Count; i++)
            {
                Debug.Log($"Role {i} : {roleTypes[i]}");
                sharedDatas[i].AssignJobRPC(roleTypes[i]);
            }    
        }
    }
    
    private void Awake()
    {
        RoleDict.Add(RoleType.Environment, new Role("환경 과학자", "행성의 대기와 생태계를 조정하고, 인간이 살 수 있는 환경을 구축하는 전문가."));
        RoleDict.Add(RoleType.Society, new Role("사회학자", "새로운 사회 구조와 문명을 설계하여, 인류가 새로운 행성에서 평화롭게 살아갈 수 있는 기반을 마련하는 전문가."));
        RoleDict.Add(RoleType.Technology, new Role("기술 엔지니어", "테리포밍을 가능하게 할 첨단 기술을 개발하고, 우주에서의 생존을 위한 인프라를 구축하는 전문가."));
        RoleDict.Add(RoleType.Economy, new Role("경제학자", "행성 자원 관리와 경제 시스템을 설계하여, 지속 가능한 경제 발전을 이끄는 전문가."));
    }

}
