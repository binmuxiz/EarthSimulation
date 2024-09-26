using Global;

namespace Home
{
    public class StorySelectionManager : Singleton<StorySelectionManager>
    {
        public enum StoryType: int
        {
            NovaTerra,
            EarthRestoration,
            EarthShift
        }
        public int StoryNum { get; private set; } = -1;

        private HomeUIController _uiController;

        private void Start()
        {
            _uiController = HomeUIController.Instance;
        }

/*
 * --------- On Clicked ------------ 
 */
        public void SelectStory(int num)
        {
            StoryNum = num;
            
            EffectSoundManager.Instance.ButtonEffect(); // 버튼 누르는 소리 
            StartCoroutine(_uiController.HideStorySelection());
            StartCoroutine(_uiController.ShowLoginMenu());
        }
    }
}