using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private const string base_url = "https://eternal-leopard-hopelessly.ngrok-free.app";
    public static NetworkManager instance;
    public static GetData _getData;
    public static SendData _sendData;
    public NetworkPrefabRef playerPrefab;


    public static string gettedJson;
    
    
   
    
    private void Awake()
    {
        if (instance == null) instance = this;
        _sendData = new SendData();
        RunnerController.Runner.SpawnAsync(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    public async UniTask<string> StartSendDataProcess()
    {
        string temp = "";
        var req = UnityWebRequest.Get(base_url + "/start");
        Debug.Log("요청 보냄");
        await req.SendWebRequest();
        Debug.Log("응답 생성됨");
        temp = req.downloadHandler.text;
        return temp;
    }
    
    public async UniTask<string> SendDataProcess()
    {
        var temp = "";
        var json = JsonConvert.SerializeObject(_sendData);
        var req = UnityWebRequest.Post(base_url + "/next", json, "application/json");
        Debug.Log("요청 보냄");
        await req.SendWebRequest();
        temp = req.downloadHandler.text;
        return temp;
    }


    
    //public IEnumerator SendDataProcess()
    //{
    //    var json = JsonConvert.SerializeObject(_sendData);
    //    var req = UnityWebRequest.Post(base_url + "/next", json, "application/json");
    //    Debug.Log("요청 보냄");
    //    yield return req.SendWebRequest();
    //    var temp = req.downloadHandler.text;
    //    //_getData = JsonConvert.DeserializeObject<GetData>(temp);
    //}
    
    //public IEnumerator StartSendDataProcess()
    //{
    //    string temp = "";
    //    if (RunnerController.Runner.IsSceneAuthority)
    //    {
    //        var req = UnityWebRequest.Get(base_url + "/start");
    //        Debug.Log("요청 보냄");
    //        yield return req.SendWebRequest();
    //        Debug.Log("응답 생성됨");
    //        temp = req.downloadHandler.text;
    //    }
    //   
    //    Debug.Log(temp);
    //    //_getData = JsonConvert.DeserializeObject<GetData>(temp);
    //}
}
