using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Global;
using Network;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoteManager : Singleton<VoteManager>
{
    
    public TMP_Text[] voteTexts; // 투표 개수 보이는 텍스트 
    public TMP_Text timerText;

    
    private bool _voted;
    private int _myVote = -1;
    
    private const int Timer = 10;

    
    private void OnEnable()
    {
        Debug.Log("OnEnable()");
        SharedData.onVoted += RefreshUI;
    }
    
        
    private void RefreshUI(Dictionary<int, int> votes)
    {
        for (var i = 0; i < votes.Count; ++i)
        {
            voteTexts[i].text = $"{votes[i]}";
        }
    }

    // private void OnDisable()
    // {
    //     SharedData.Instance.OnVoted -= RefreshUI;
    // }
    
    public async UniTask<int> VoteProcess()
    {
        SharedData.HasAggregated = false;
        await TimerProcess(); 
        RandomChoice(); // 아무 선택이 없을 때 랜덤 선택 (내 선택만)
            
        // await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

        int maxChoice = 0;
        if (RunnerController.Runner.IsSharedModeMasterClient) // 투표의 일관성을 위해 마스터 클라이언트가 투표 집계 
        { 
            maxChoice = MaxChoice();
            NetworkManager.Instance.SendData.choice_index = maxChoice;
            SharedData.HasAggregated = true;
            SharedData.Instance.RpcHasAggregated();
        }
        else
        {
            await UniTask.WaitUntil(() => SharedData.HasAggregated);
        }
            
        ClearMyVote();
        return maxChoice;
    }
    
    
    private int MaxChoice()
    {
        int maxChoiceIndex;
            
        Dictionary<int, int> votes = SharedData.Votes;
            
        // case 1111
        if (votes[0] == 1 && votes[1] == 1 && votes[2] == 1 && votes[3] == 1)
        {
            maxChoiceIndex = Random.Range(0, 4);
        }
        else
        {
            // votes 내림차순 정렬
            var sortedVotes = votes.OrderByDescending(x => x.Value);

            // case 2200
            var first = sortedVotes.First();
            var second = sortedVotes.ElementAt(1);

            if (first.Value == 2 && second.Value == 2)
            { 
                int rand = Random.Range(0, 2);
                    
                if (rand == 0) maxChoiceIndex = first.Key;
                else maxChoiceIndex = second.Key;
            }
            else
            {
                maxChoiceIndex = first.Key;
            }
        }
        
        return maxChoiceIndex;
    }
    
    
    private void RandomChoice()
    {
        if (!_voted)
        {
            Vote(Random.Range(0, NetworkManager.ChoiceNum));
        } 
    }
    
    
    public void Vote(int idx)
    {
        if (_myVote == idx) return; // 동일 선택지 선택시 return
    
        SharedData.Instance.RpcVote(idx);
        if (_voted) SharedData.Instance.RpcVoteCancel(_myVote);    
    
        _myVote = idx;
        _voted = true;
    }


    private void ClearMyVote()
    {
        _voted = false;
        _myVote = -1;
    }
    
    
    private async UniTask TimerProcess()
    {
        for (int t = Timer; t >= 0; t--)
        {
            timerText.text = $"남은 시간 : {t}";
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
