using System;
using Cysharp.Threading.Tasks;
using Global;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        private const string base_url = "https://eternal-leopard-hopelessly.ngrok-free.app";

        private SendData _sendData;
        public GetData GetData { get; set; }

        public const int ChoiceNum = 4; 

        public SendData SendData
        {
            get => _sendData;
            set => _sendData = value;
        }
    
        private void Awake()
        {
            GetData = null;
            _sendData = new SendData();
        }

    
// 처음 스토리 요청
        public async UniTask<string> RequestStartData()
        {
            var req = UnityWebRequest.Get(base_url + "/start");
            Debug.Log("요청 보냄");
            await req.SendWebRequest();
            Debug.Log("응답 생성됨"); 
        
            string received = req.downloadHandler.text;
            GetData = JsonConvert.DeserializeObject<GetData>(received);

            return received;
        }

// 이후 스토리 요청
        public async UniTask<string> RequestData()
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
        
// 엔딩 요청

        // public async UniTask<string> RequestEnding()
        // {
        //     
        // }

        public string[] GetStory()
        {
            string[] story = GetData.story.Split(".");

            if (story == null)
            {
                throw new NullReferenceException("GetData is Null");
            }
            
            for (int j = 0; j < story.Length - 1; j++)
            {
                story[j] += '.';
            }

            return story;
        }
    
        public string[] GetChoices()
        {
            string[] texts = new string[ChoiceNum];

            int i = 0;
            foreach (var choice in GetData.choices)
            {
                if (i == ChoiceNum) break;
                texts[i++] = choice.text;            
            }

            return texts;
        }

        public Score GetScore(int idx)
        {
            return GetData.choices[idx].score;
        }
    }
}
