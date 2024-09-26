using System.Collections;
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
        roleImage.sprite = roleSprites[(int) SharedData.Instance.Role];
        yield return ShowStory();
        yield return ShowRole();
    }

    private IEnumerator ShowStory()
    {
        yield return FadeController.FadeIn(storyCanvasGroup, 0.6f);
        yield return textLoader.LoadText("game_story", storyText, true);
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeController.FadeOut(storyCanvasGroup, 0.6f));
    }

    private IEnumerator ShowRole()
    {
        yield return FadeController.FadeIn(roleCanvasGroup, 0.6f);
        yield return new WaitForSeconds(3f);
        yield return FadeController.FadeOut(roleCanvasGroup, 1f);
    }
}
