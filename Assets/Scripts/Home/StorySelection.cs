using UnityEngine;

namespace Home
{
    public class StorySelection : MonoBehaviour
    {
        [SerializeField]
        private GameStarter gameStarter;
        
        // 추후에 
        // story : 세션 그룹을 나누는 기준
        
        public void SelectStoryNovaTerra()
        {
            EffectSoundManager.Instance.ButtonEffect();

            StartCoroutine(HomeUIManager.Instance.HideStorySelection());

            gameStarter.gameStory = GameStarter.GameStory.NovaTerra;
            
            StartCoroutine(HomeUIManager.Instance.ShowGameStartMenu());
        }
    }
}