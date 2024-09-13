using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private const string base_url = "https://eternal-leopard-hopelessly.ngrok-free.app";
    
    public static NetworkManager instance;
    public GetData _getData;
    public SendData _sendData;
    
    private void Awake()
    {
        _sendData = new SendData();
        if (instance == null)
            instance = this;
    }

    public IEnumerator SendDataProcess()
    {
        var json = JsonConvert.SerializeObject(_sendData);
        var req = UnityWebRequest.Post(base_url + "/next", json, "application/json");
        Debug.Log("요청 보냄");
        yield return req.SendWebRequest();
        var temp = req.downloadHandler.text;
        _getData = JsonConvert.DeserializeObject<GetData>(temp);
    }

    public IEnumerator StartSendDataProcess() 
    {
        var req = UnityWebRequest.Get(base_url + "/start");
        Debug.Log("요청 보냄");
        yield return req.SendWebRequest();
        Debug.Log("응답 생성됨");

        var temp = req.downloadHandler.text;
        
        _getData = JsonConvert.DeserializeObject<GetData>(temp);
    }
}
