using System;
using System.Collections.Generic;
using Global;
using UnityEngine;

namespace Multi
{
    public class SharedDataList : Singleton<SharedDataList>
    {
        [SerializeField] 
        private List<SharedData> _sharedDatas = new List<SharedData>();

        public List<SharedData> SharedDatas => _sharedDatas;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void AddSharedData(SharedData sharedData)
        {
            _sharedDatas.Add(sharedData);
            
            Debug.Log("AddSharedData");
        }
    } 
}