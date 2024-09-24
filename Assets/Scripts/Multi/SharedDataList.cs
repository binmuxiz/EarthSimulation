using System.Collections.Generic;
using Global;

namespace Multi
{
    public class SharedDataList : Singleton<SharedDataList>
    {
        public List<SharedData> sharedDatas;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}