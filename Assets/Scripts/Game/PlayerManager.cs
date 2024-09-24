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
        public Sprite[] sprites = new Sprite[4];

        public Image[] roleImages = new Image[4];
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
                    roleImages[0].sprite = GetSprite(sharedData.Role.ToString());
                }
                else
                {
                    nickNames[i].text = sharedData.NickName;
                    roles[i].text = dict[sharedData.Role].Name;
                    roleImages[i].sprite = GetSprite(sharedData.Role.ToString());
                    
                    i++;
                }
            }
        }

        private Sprite GetSprite(string name)
        {
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == name) return sprite;

            }
            Log.Error($"Cannot found Sprite with Name '{name}'");
            return null;
        }
    }
}