using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private const string base_url = "https://eternal-leopard-hopelessly.ngrok-free.app";

    private static GetData _getData;
    private static SendData _sendData;

    public static GetData GetData
    {
        get => _getData;
        set => _getData = value;
    }

    public static SendData SendData
    {
        get => _sendData;
        set => _sendData = value;
    }

    private void Awake()
    {
        _sendData = new SendData();
    }

    public static async UniTask<string> RequestStartData()
    {
        var req = UnityWebRequest.Get(base_url + "/start");
        Debug.Log("요청 보냄");
        await req.SendWebRequest();
        Debug.Log("응답 생성됨"); 
        
        string received = req.downloadHandler.text;
        GetData = JsonConvert.DeserializeObject<GetData>(received);

        return received;
    }
    
    public static async UniTask<string> RequestData()
    {
        var json = JsonConvert.SerializeObject(SendData);
        
        var req = UnityWebRequest.Post(base_url + "/next", json, "application/json");
        Debug.Log("요청 보냄");
        await req.SendWebRequest();
        Debug.Log("응답 생성됨");

        var received = req.downloadHandler.text;
        GetData = JsonConvert.DeserializeObject<GetData>(received);
        
        return received;
    }
}
