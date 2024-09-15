using System.Collections;
using TMPro;
using UnityEngine;

public class TextLoader : MonoBehaviour
{
    public TypingEffect typingEffect;

    private void Start()
    {
        Debug.Log("TypingEffect is exist : " + (typingEffect != null));
        
        // DontDestroyOnLoad(gameObject);
    }

    public IEnumerator LoadText(string filename, TMP_Text tmpText, bool effect)
    {
        var textAsset = Resources.Load<TextAsset>(filename);

        if (textAsset != null)
        {
            Debug.Log(textAsset.text);

            if (effect)
            {
                yield return StartCoroutine(typingEffect.TypeText(textAsset.text, tmpText));
            }
            else
            {
                tmpText.text = textAsset.text;
            }
        }
        else
        {
            Debug.LogError("파일을 찾을 수 없습니다.");
        }
    }
}
