using System.Collections.Generic;
using Data;
using Fusion;
using Global;
using Multi;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public TMP_Text[] nickNames = new TMP_Text[4];

        public void SetPlayersInfo()
        {
            foreach (SharedData sharedData in SharedDataList.Instance.sharedDatas)
            {
                if (sharedData == null)
                {
                    Debug.LogError("sharedData is null !!!");
                    continue;
                }
                
                Debug.Log($"sharedData.NickName: {sharedData.NickName}, Role: {sharedData.Role}");

                int idx = (int) sharedData.Role;                
                
                if (sharedData.HasStateAuthority)
                {
                    nickNames[idx].text = $"(나) {sharedData.NickName}";
                }
                else
                {
                    nickNames[idx].text = sharedData.NickName;
                }
            }
        }
    }
}