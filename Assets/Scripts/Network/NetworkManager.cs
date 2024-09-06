using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
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
        var req = UnityWebRequest.Post("https://87a2-59-13-225-125.ngrok-free.app/next", json, "application/json");
        Debug.Log("요청 보냄");
        yield return req.SendWebRequest();
        Debug.Log("응답 생성됨");
        var temp = req.downloadHandler.text;
         _getData = JsonConvert.DeserializeObject<GetData>(temp);
         Debug.Log("-------------------");
         Debug.Log(_getData);
         Debug.Log(_getData.text);
    }

    public IEnumerator StartSendDataProcess() 
    {
        var req = UnityWebRequest.Get("https://87a2-59-13-225-125.ngrok-free.app/start/");
        Debug.Log("요청 보냄");
        yield return req.SendWebRequest();
        Debug.Log("응답 생성됨");

        string temp = req.downloadHandler.text; 
        _getData = JsonConvert.DeserializeObject<GetData>(temp);
    }
}
