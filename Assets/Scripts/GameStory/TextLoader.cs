using System;
using TMPro;
using UnityEngine;

public class TextLoader : MonoBehaviour
{
    public TMP_Text storyText;
    public TypingEffect typingEffect;
    
    private void Start()
    {
        var textAsset = Resources.Load<TextAsset>("game_story");

        if (textAsset != null)
        {
            Debug.Log(textAsset.text);
            StartCoroutine(typingEffect.TypeText(textAsset.text, storyText));
        }
        else
        {
            Debug.LogError("파일을 찾을 수 없습니다.");
        }
    }
}
