using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Main
{
    public class HomeManager : MonoBehaviour
    {
        // UnityEvent를 사용하여 LoadingCanvas 활성화 제어
        public UnityEvent onGameStart;
    
        // public void StartGame()
        // {
        //     Debug.Log("GameManager.StartGame()");
        //
        //     onGameStart.Invoke();
        //     StartCoroutine(LoadGameStoryScene());
        // }
    
        public void EnterGame(string RoomName = null)
        {
            Debug.Log("GameManager.StartGame()");

            onGameStart.Invoke();
        
            FusionManager.Instance.StartGame(RoomName);
        
            // todo GameScene으로 전환 
        }
    
        // private IEnumerator LoadGameStoryScene()
        // {
        //     Debug.Log("GameManager.LoadGameScene()");
        //     
        //     // todo 모든 인원이 찰 떄까지 대기
        //     yield return new WaitForSeconds(3);
        //     SceneManager.Instance.LoadScene("Game Scene");        
        // }
    
        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}
