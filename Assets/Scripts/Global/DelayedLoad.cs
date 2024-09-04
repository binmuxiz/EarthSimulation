using System.Collections;
using UnityEngine;

namespace Global
{
    public class DelayedLoad : MonoBehaviour
    {
        public float delayTime = 1f;

        private IEnumerator Start()
        {
            Debug.Log("Loading Delayed...");

            yield return new WaitForSeconds(delayTime);
        
            Debug.Log("Loading Done...");

            SceneManager.isLoaded = true;
        }
    }
}
