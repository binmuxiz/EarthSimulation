using System.Collections;
using Global;
using UnityEngine;

namespace Home
{
    public class StorySelection : Singleton<StorySelection>
    {
        // 추후에 
        // story : 세션 그룹을 나누는 기준
        
        public IEnumerator Story3()
        {
            yield return HomeUIManager.Instance.HideStorySelection();
            MiniMenu.Instance.StoryNum = 3;
            yield return HomeUIManager.Instance.ShowMiniMenu();
        }
    }
}