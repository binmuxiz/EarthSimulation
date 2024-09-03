using System.Collections;
using UnityEngine;

public class IntroCanvas : MonoBehaviour
{
    public SceneManager sceneManager;
    public CanvasGroup introCanvasGroup;
    public FadeController fadeController;

    private IEnumerator Start()
    {
        yield return fadeController.FadeIn(introCanvasGroup);

        yield return new WaitForSeconds(1.3f);

        yield return fadeController.FadeOut(introCanvasGroup);

        sceneManager.LoadScene("Main Scene");
    }
}
