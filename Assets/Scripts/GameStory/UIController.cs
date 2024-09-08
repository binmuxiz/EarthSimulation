using System.Collections;
using Global;
using UnityEngine;

namespace GameStory
{
    public class UIController : MonoBehaviour
    {
        public GameObject storyPanel;
        public GameObject rolePanel;
        public FadeController fadeController;

        private StoryUI storyUI;
        private RoleUI roleUI;


        private void Awake()
        {
            storyPanel.SetActive(true);
            rolePanel.SetActive(true);
            
            storyUI = storyPanel.GetComponent<StoryUI>();
            roleUI = rolePanel.GetComponent<RoleUI>();
        }

        private IEnumerator Start()
        {
            yield return storyUI.Show(fadeController);
            yield return storyUI.Hide(fadeController);

            yield return new WaitForSeconds(1f);
            
            yield return roleUI.Show(fadeController);
            yield return roleUI.Hide(fadeController);

        }
        
        // public IEnumerator ShowRoleUI()
        // {
        //     yield return storyUI.Hide(fadeController);
        //
        //     yield return new WaitForSeconds(2f);
        //     
        //     yield return roleUI.Show(fadeController);
        // }
        //
        // public IEnumerator HideRoleUI()
        // {
        //     yield return roleUI.Hide(fadeController);
        // }
    }
}
