using Cysharp.Threading.Tasks;
using Data;
using Home;
using Multi;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;

public class LoginManager : MonoBehaviour
{
    private HomeUIController _uiController;

    private const string LoadingSceneName = "Loading Scene";

    [SerializeField] TMP_InputField nickName;
    [SerializeField] TMP_InputField roomName;

    void Start()
    {
        _uiController = HomeUIController.Instance;
        nickName.text = "";
        roomName.text = "";
    }

/*
 * ----------- On Clicked -------------
 */

    public void Enter()
    {
        Debug.Log("Enter()");
        Process().Forget();
    }

    private async UniTask Process()
    {
        Debug.Log("Enter() -> Process()");
        
        EffectSoundManager.Instance.ButtonEffect();  // 버튼 클릭 소리 

        if (nickName.text.IsNullOrEmpty())
        {
            Debug.Log("닉네임을 입력하세요");
            return;
        }
        if (string.IsNullOrEmpty(roomName.text))
        {
            roomName.text = null; // 빈 문자열 이름의 방이 생성되지 않고, null로 초기화하여 임의방 접속/생성 하도록 
        }
        NickName.value = nickName.text;

        await _uiController.HideLoginMenu();
        await _uiController.ShowConnectingView(); 
        
        RoomCreator.Instance.CreateRoom(roomName.text, StorySelectionManager.Instance.StoryNum, LoadingSceneName);
    }
}