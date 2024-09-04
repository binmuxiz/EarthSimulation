using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    
    public static bool isLoaded;
    public string emptySceneName = "Empty";

    private Fader _fader;

    /*
     * string.Empty : 길이가 0인 문자열 ""
     */
    private string Current { get; set; } = string.Empty;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else
        {
            Debug.Log("Destroy SceneManager GameObject");
            Destroy(gameObject);
        }
        
        
        Scene activeScene = UnitySceneManager.GetActiveScene();
        Current = activeScene.name;

        _fader = GetComponent<Fader>();

        // 씬이 넘나들어도 SceneManager는 Destory되지 않도록 
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadProcess(sceneName));
    }

    private IEnumerator LoadProcess(string sceneName)
    {
        if (Current == sceneName) yield break; // 현재 씬을 또 로드하려는 경우 

        Debug.Log($"LoadScene {sceneName}");

        var showCoroutine = StartCoroutine(_fader.Show());

        /*
         * LoadSceneMode.Single : 현재 로드된 모든 씬을 종료하고, 지정한 씬을 로드한다.  
         * LoadSceneMode.Additive : 현재 씬을 UnLoad하지 않고, 지정한 씬을 추가로 로드한다.  
         */
        // 빈 씬을 로드 
        yield return UnitySceneManager.LoadSceneAsync(emptySceneName, LoadSceneMode.Additive);

        // 현재 로드된 씬이 있다면, 현재 씬을 Unload한다.
        if (!string.IsNullOrWhiteSpace(Current))
        {
            yield return UnitySceneManager.UnloadSceneAsync(Current);
        }

        // 지정한 씬을 로드
        yield return UnitySceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        // var asyncOperation = UnitySceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        // asyncOperation.allowSceneActivation = true;
        // while (!asyncOperation.isDone)
        // {
        //     var p = asyncOperation.progress / 0.9f;
        //     Debug.Log(p);
        //     //////
        // }
        
        Current = sceneName;

        isLoaded = false;
        // isLoaded가 true가 될 때까지 기다린다.
        // 새로 로드하는 씬 쪽에서 로딩이 완료된 후 isLoaded를 true로 변경
        yield return new WaitUntil(() => isLoaded);

        // 빈 씬을 언로드 
        yield return UnitySceneManager.UnloadSceneAsync(emptySceneName);

        StopCoroutine(showCoroutine);
        yield return _fader.Hide();
    }
}
