using System;
using System.Collections;
using Global;
using UnityEditor;
using UnityEngine;

namespace Home
{
    public class HomeManager : Singleton<HomeManager>
    {
        private HomeUIController _uiController;
        
        public IEnumerator Process()
        {
            _uiController = HomeUIController.Instance;
            
            yield return _uiController.ShowIntro();

            yield return new WaitForSeconds(2f);
            
            yield return _uiController.HideIntro();
            
            yield return _uiController.ShowMenu();
            
            // introUI.gameObject.SetActive(false);                                          
        }
        
/*
 *     ---------- On Clicked Menu Button ------------- 
 */
        public void StartGame()
        {
            StartCoroutine(StartGameProcess());
        }

        private IEnumerator StartGameProcess()
        {
            EffectSoundManager.Instance.ButtonEffect();

            yield return new WaitForSeconds(1f);
            
            StartCoroutine(_uiController.HideMenu());
            StartCoroutine(_uiController.ShowStorySelection());
        }
        

        public void QuitGame()
        {
            EffectSoundManager.Instance.ButtonEffect();

            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}