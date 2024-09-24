using System.Collections.Generic;
using Data;
using Global;
using Multi;
using TMPro;
using UnityEngine;

namespace Game
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        // todo 이미지 오브젝트들
        
        public TMP_Text[] nickNames = new TMP_Text[4];
        public TMP_Text[] roles = new TMP_Text[4];

        public void SetPlayersInfo()
        {
            Debug.Log("SetPlayerInfo()");
            
            Dictionary<RoleType, Role> dict = GameManager.Instance.RoleDict;
            int i = 1;
            
            foreach (SharedData sharedData in SharedDataList.Instance.sharedDatas)
            {
                Debug.Log($"sharedData.NickName: {sharedData.NickName}, Role: {sharedData.Role}");
                if (sharedData.HasStateAuthority)
                {
                    nickNames[0].text = $"(나) {sharedData.NickName}";
                    roles[0].text = dict[sharedData.Role].Name;
                }
                else
                {
                    nickNames[i].text = sharedData.NickName;
                    roles[i].text = dict[sharedData.Role].Name;
                    i++;
                }
            }
        }
    }
}