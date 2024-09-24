using Global;
using TMPro;
using UnityEngine;

public class MessageManager : Singleton<MessageManager>
{
    public CanvasGroup messageCanvas;
    
    private const string WaitOtherClient = "다른 팀원이 스토리를 읽는 중입니다.";

    private TMP_Text _message;

    private void Awake()
    {
        messageCanvas.gameObject.SetActive(false);
        _message = messageCanvas.gameObject.GetComponentInChildren<TMP_Text>();
    }

    public void ShowWaitOtherClientMessage()
    {
        messageCanvas.gameObject.SetActive(true);
        _message.text = WaitOtherClient;
    }
    
    public void HideWaitOtherClientMessage()
    {
        messageCanvas.gameObject.SetActive(false);
        _message.text = null;
    }
}
