using System.Collections;
using UnityEngine;

namespace Global
{
    public class DelayedLoad : MonoBehaviour
    {
        [SerializeField] private float delayTime = 2f;

        private IEnumerator Start()
        {
            Debug.Log("Loading Delayed...");

            // todo delayTime말고 씬 로딩이 다 되었는지 확인하는 방법은 없나??
            yield return new WaitForSeconds(delayTime);
        
            Debug.Log("Loading Done...");

            // SceneManager.isLoaded = true;
        }
    }
}
