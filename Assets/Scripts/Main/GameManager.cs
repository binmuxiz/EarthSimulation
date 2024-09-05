#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
