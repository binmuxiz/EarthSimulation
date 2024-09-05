using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Global
{
    public class SceneManager : Singleton<SceneManager>
    {
        // public static bool isLoaded;
        public string emptySceneName = "Empty";

        private Fader _fader;

        private string Current { get; set; } = string.Empty;

        private void Awake()
        {
            // 씬이 넘나들어도 SceneManager는 Destory되지 않도록 

            _fader = GetComponent<Fader>();

            Scene activeScene = UnitySceneManager.GetActiveScene();
            Current = activeScene.name;
            
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
            yield return LoadSceneAsyncRoutine(emptySceneName); // 빈 씬 로드
            // yield return UnitySceneManager.LoadSceneAsync(emptySceneName, LoadSceneMode.Additive);

            if (!string.IsNullOrWhiteSpace(Current))
            {
                yield return UnitySceneManager.UnloadSceneAsync(Current);
            }

            yield return LoadSceneAsyncRoutine(sceneName);

            Current = sceneName;
            // isLoaded = false;
            // // isLoaded가 true가 될 때까지 기다린다.
            // // 새로 로드하는 씬 쪽에서 로딩이 완료된 후 isLoaded를 true로 변경
            // yield return new WaitUntil(() => isLoaded);

            yield return UnitySceneManager.UnloadSceneAsync(emptySceneName);

            StopCoroutine(showCoroutine);
            yield return _fader.Hide();
        }
        
        private IEnumerator LoadSceneAsyncRoutine(string sceneName)
        {
            AsyncOperation asyncOperation = UnitySceneManager.LoadSceneAsync(sceneName);
            // 씬이 완료 될 때까지 대기
            while (!asyncOperation.isDone)
            {
                var p = asyncOperation.progress * 100;
                Debug.Log(p + "%");
                yield return null;
            }
            
            Debug.Log("Scene Loading Completed!");
        }
    }
}
