using UnityEngine;

public class EarthCamera : MonoBehaviour
{
    void Start()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            canvas.worldCamera = Camera.main;
        }
        else
        {
            Debug.LogWarning("Canvas의 Render Mode가 Screen Space - Camera가 아닙니다.");
        }
    }
    
}
