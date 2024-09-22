using System.Collections;
using Fusion;
using Multi;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingRoom : MonoBehaviour
{
    public NetworkPrefabRef sharedDataPrefab;
    
    public Button readyButton;
    public TMP_Text readyText;
    
    private bool _isReady;

    private const string GameSceneName = "Game Scene";

    public void Ready()
    {
        _isReady = true;
        readyButton.interactable = false;
        SharedData.Instance.RpcReady();
    }
    
    private void Start()
    {
        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        // 초기화
        _isReady = false;
        readyButton.gameObject.SetActive(true);
        readyText.gameObject.SetActive(true);
        readyText.text = "";
        
        // SharedData 스폰
        var dataOp = RunnerController.Runner.SpawnAsync(sharedDataPrefab);
        yield return new WaitUntil(() => dataOp.Status == NetworkSpawnStatus.Spawned);
        dataOp.Object.name = $"{nameof(SharedData)}: {dataOp.Object.Id}";
        
        RunnerController.Runner.SetPlayerObject(RunnerController.Runner.LocalPlayer, dataOp.Object);
        
        // 모든 플레이어가 ready 할 때까지 대기
        var wfs = new WaitForSeconds(0.5f);
        while (true)
        {
            var totalCount = RunnerController.Runner.SessionInfo.PlayerCount;
            var currentCount = SharedData.ReadyCount;
            readyText.text = $"Ready?\n{currentCount}/{totalCount}";

            yield return wfs;

            if (currentCount == totalCount) break;
        }
        readyText.text = $"Ready?\n{SharedData.ReadyCount}/{RunnerController.Runner.SessionInfo.PlayerCount}";
        yield return wfs;

        // 모든 SharedData를 리스트에 추가 
        foreach (var netObj in RunnerController.Runner.GetAllNetworkObjects())
        {
            var sharedData = netObj.GetComponent<SharedData>();

            if (sharedData != null)
            {
                PlayerManager.Instance.players.Add(sharedData);
            }
        }

        // 게임 씬으로 이동 
        if (RunnerController.Runner.IsSharedModeMasterClient)
        {
            RunnerController.Runner.LoadScene(SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath(GameSceneName)));
        }
    }
}
