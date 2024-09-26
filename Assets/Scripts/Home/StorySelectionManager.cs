using Cysharp.Threading.Tasks;
using Global;

namespace Home
{
    public class StorySelectionManager : Singleton<StorySelectionManager>
    {
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
            _uiController.ShowSelectStoryUI(StoryNum + 1);
            await UniTask.WaitForSeconds(2f); // todo 초 수정
            
            StartCoroutine(_uiController.HideStorySelection());
            MoveToLoginMenu();
        }
            

        private void MoveToLoginMenu()
        {
            StartCoroutine(_uiController.ShowLoginMenu());
            _uiController.InitializeSelectStoryUI();
        }
    }
}