using System.Collections.Generic;
using Global;

namespace Multi
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public List<SharedData> players;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}