using Cysharp.Threading.Tasks;
using Global;

namespace Game
{
    public class EndingManager : Singleton<EndingManager>
    {
        public bool processPermitted = false;
        private async void Start()
        {
            await UniTask.WaitUntil(() => processPermitted);
            StoryProcess();
        }

        private void StoryProcess()
        {
        
        }

    }
}