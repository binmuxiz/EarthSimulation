using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public float typingSpeed = 0.1f;
    
    public IEnumerator TypeText(string text, TMP_Text uiText)
    {
        foreach (char letter in text)
        {
            uiText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
