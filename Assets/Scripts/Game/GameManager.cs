using System.Collections;
using GameStory;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameIntroUIController gameIntroUIController;

    private IEnumerator Start()
    {
        yield return gameIntroUIController.ShowIntro();
        
        // TODO intro 각자 본 이후 게임 시작
        SharedData.Instance.CheckReadStoryDoneRpc();
        yield return new WaitUntil(() => SharedData.MaxCount <= SharedData.CountReadStoryDone);
        Debug.Log("모두 다 읽음");
        Debug.Log(UIManager.storyPermitted);
           
        SharedData.Instance.ClearReadCountRpc();
        //if(RunnerController.Runner.IsSceneAuthority)
    }
}
