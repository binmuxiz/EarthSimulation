using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStory
{
    public class RoleDatabase : MonoBehaviour
    {
        private List<PlayerRole> list;

        private void Awake()
        {
            list = new List<PlayerRole>();
            
        }

        private void Start()
        {
            list.Add(new PlayerRole("아리아 박사", "환경 과학자", "행성의 대기와 생태계를 조정하고, 인간이 살 수 있는 환경을 구축하는 전문가"));
            list.Add(new PlayerRole("다니엘 교수", "사회학자", "새로운 사회 구조와 문명을 설계하여, 인류가 새로운 행성에서 평화롭게 살아갈 수 있는 기반을 마련하는 전문가"));
        }
    }
}
