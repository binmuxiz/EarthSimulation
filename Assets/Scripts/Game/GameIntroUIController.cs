using System.Collections;
using Data;
using Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameIntroUIController : MonoBehaviour
{
    public CanvasGroup storyCanvasGroup;

    public TMP_Text storyText;
    public TextLoader textLoader;

    public CanvasGroup roleCanvasGroup;
    [SerializeField] private Image roleImage;
    [SerializeField] private Sprite[] roleSprites;
    
    private void Awake()
    {
        storyText.text = null;
        textLoader = GetComponent<TextLoader>();
        
        storyCanvasGroup.gameObject.SetActive(true);
        roleCanvasGroup.gameObject.SetActive(true);

        storyCanvasGroup.alpha = 0f;
        roleCanvasGroup.alpha = 0f;

        storyCanvasGroup.interactable = storyCanvasGroup.blocksRaycasts = false;
        roleCanvasGroup.interactable = roleCanvasGroup.blocksRaycasts = false;
    }

    public IEnumerator ShowIntro()
    {
        yield return ShowStory();
        yield return ShowRole();
    }

    private IEnumerator ShowStory()
    {
        float fadeDuration = 0.6f;
        yield return FadeController.FadeIn(storyCanvasGroup, fadeDuration);
        yield return textLoader.LoadText("game_story", storyText, true);
        yield return new WaitForSeconds(3f);
        yield return FadeController.FadeOut(storyCanvasGroup, fadeDuration);
    }

    private IEnumerator ShowRole()
    {
        roleImage.sprite = roleSprites[(int) SharedData.Instance.Role];
        
        float fadeDuration = 0.6f;
        yield return FadeController.FadeIn(roleCanvasGroup, fadeDuration);
        yield return new WaitForSeconds(5f);
        yield return FadeController.FadeOut(roleCanvasGroup, fadeDuration);
    }
}
