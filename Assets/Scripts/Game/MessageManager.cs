using System.Collections;
using Global;
using TMPro;
using UnityEngine;

public class MessageManager : Singleton<MessageManager>
{
    [SerializeField] private CanvasGroup messageCanvasGroup;
    [SerializeField] private TMP_Text waitingMessage;

    private const string WaitOtherClientMessage = "다른 팀원이 스토리를 읽는 중입니다.";

    private void Awake()
    {
        messageCanvasGroup.gameObject.SetActive(true);
        waitingMessage.text = WaitOtherClientMessage;
    }

    public void ShowWaitOtherClientMessage()
    {
        messageCanvasGroup.alpha = 1f;
    }
    
    public void HideWaitOtherClientMessage()
    {
        messageCanvasGroup.alpha = 0f;
    }
}
