using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public float typingSpeed = 0.1f;

    private bool skip = false;

    public bool Skip
    {
        get => skip;
        set => skip = value;
    }

    public IEnumerator TypeText(string str, TMP_Text uiText)
    {
        int i = 0;
        for (; i < str.Length; i++)
        {
            if (!skip)
            {
                uiText.text += str[i];
                yield return new WaitForSeconds(typingSpeed);
            }
            else break;
        }

        if (i != str.Length)
        {
            uiText.text = str.Substring(i);
        }
    }
}
