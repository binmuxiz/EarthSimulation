using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    
    private static GetData _getData;
    private static SendData _sendData;
    
    private const string base_url = "https://eternal-leopard-hopelessly.ngrok-free.app";

    public static GetData GetData
    {
        get => _getData;
        set => _getData = value;
    }
    
    private void Awake()
    {
        if (instance == null) instance = this;
        _sendData = new SendData();
    }

    public async UniTask<string> RequestStartData()
    {
        var req = UnityWebRequest.Get(base_url + "/start");
        Debug.Log("요청 보냄");
        await req.SendWebRequest();
        Debug.Log("응답 생성됨"); 
        
        string data = req.downloadHandler.text;
        GetData = JsonConvert.DeserializeObject<GetData>(data);

        return data;
    }
    
    public async UniTask<string> RequestData()
    {
        var temp = "";
        var json = JsonConvert.SerializeObject(_sendData);
        var req = UnityWebRequest.Post(base_url + "/next", json, "application/json");
        Debug.Log("요청 보냄");
        await req.SendWebRequest();
        temp = req.downloadHandler.text;
        return temp;
    }
}
