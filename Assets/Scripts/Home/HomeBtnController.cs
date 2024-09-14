using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Home
{
    public class HomeBtnController : MonoBehaviour
    {
        public UnityEvent onGameLoading;
        
        public void StartGame(string RoomName = null)
        {
            Debug.Log("GameManager.StartGame()");

            onGameLoading.Invoke();

            FusionManager.Instance.StartGame(RoomName);

            // todo GameScene으로 전환 
        }

        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}