using System.Collections;
using Data;
using Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameIntroUIController : Singleton<GameIntroUIController>
{
    public CanvasGroup storyCanvasGroup;
    public CanvasGroup roleCanvasGroup;

    public TMP_Text storyText;
    public TextLoader textLoader;

    public Image roleImage;
    public TMP_Text roleName;
    public TMP_Text roleDescription;
    
    private void Awake()
    {
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
        yield return new WaitForSeconds(1f);
        yield return ShowRole();
    }

    private IEnumerator ShowStory()
    {
        float fadeDuration = 0.6f;
        yield return FadeController.Instance.FadeIn(storyCanvasGroup, fadeDuration);
        yield return textLoader.LoadText("game_story", storyText, true);
        yield return new WaitForSeconds(3f);
        yield return FadeController.Instance.FadeOut(storyCanvasGroup, fadeDuration);
    }

    private IEnumerator ShowRole()
    {
        Role myRole = GameManager.Instance.RoleDict[SharedData.Instance.Role];
        roleName.text = myRole.Name + "\nNickname 넣어야 함";
        roleDescription.text = myRole.Description;
        
        float fadeDuration = 0.6f;

        yield return FadeController.Instance.FadeIn(roleCanvasGroup, fadeDuration);
        yield return new WaitForSeconds(3f);
        yield return FadeController.Instance.FadeOut(roleCanvasGroup, fadeDuration);
    }
}
