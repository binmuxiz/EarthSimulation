using System.Collections;
using Global;
using UnityEngine;

namespace Home
{
    public class StorySelection : Singleton<StorySelection>
    {
        // 추후에 
        // story : 세션 그룹을 나누는 기준
        
        public void Story_NovaTerra()
        {
            StartCoroutine(HomeUIManager.Instance.HideStorySelection());
            MiniMenu.Instance.GameStory = GameStory.NovaTerra;
            StartCoroutine(HomeUIManager.Instance.ShowMiniMenu());
        }
    }
}