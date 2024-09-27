using Cysharp.Threading.Tasks;
using Global;
using UnityEditor;
using UnityEngine;

namespace Home
{
    public class StorySelectionManager : Singleton<StorySelectionManager>
    {
        public GameObject imagesGameObject;
        
        public int StoryNum { get; private set; } = -1;

        private HomeUIController _uiController;

        private void Start()
        {
            _uiController = HomeUIController.Instance;
        }

/*
 * --------- On Clicked ------------ 
 */
        public void SelectStory(int storyNum)
        {
            EffectSoundManager.Instance.ButtonEffect(); // 버튼 누르는 소리
            
            StoryNum = storyNum;
            
            ChangeStorySelectionUI().Forget();
        }

        private async UniTask ChangeStorySelectionUI()
        {
            await UniTask.WaitForSeconds(0.5f);
         
            imagesGameObject.SetActive(false);
            
            _uiController.ShowSelectStoryUI(StoryNum + 1);
            await UniTask.WaitForSeconds(2f);
            
            StartCoroutine(_uiController.HideStorySelection());
            
            // await UniTask.WaitForSeconds(2f); 

            MoveToLoginMenu();
        }
            

        private void MoveToLoginMenu()
        {
            StartCoroutine(_uiController.ShowLoginMenu());
            
            _uiController.InitializeSelectStoryUI();
        }
    }
}