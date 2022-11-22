using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SkyReach.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject statPanel;
        
        [SerializeField] private GameObject optionPanel;
        [SerializeField] private GameObject creditPanel;
        [SerializeField] private StatisticCanvasTracker statsUI;
        [SerializeField] private Animator transitionOutAnimator;

        private void Start()
        {
            if (statsUI == null)
                statsUI = FindObjectOfType<StatisticCanvasTracker>();
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ShowMainMenu()
        {
            DisableAllPanels();
            PlayTransitionOutAnim();
        }
        public void ShowStatPanel()
        {
            PlayTransitionInAnim();
            DisableAllPanels();
            StartCoroutine(DelayBeforeSettingActive(statPanel, 0.5f));
            statsUI.SetData();
        }
        
        public void ShowCreditPanel()
        {
            DisableAllPanels();
            StartCoroutine(DelayBeforeSettingActive(creditPanel, 0.5f));
        }
        
       public void ShowOptionPanel()
       {
           PlayTransitionInAnim();
           DisableAllPanels();
           StartCoroutine(DelayBeforeSettingActive(optionPanel, 0.5f));
       }

       private IEnumerator DelayBeforeSettingActive(GameObject panel, float sec)
       {
           yield return new WaitForSeconds(sec);
           panel.gameObject.SetActive(true);
       }
       private void PlayTransitionInAnim()
       {
           transitionOutAnimator.gameObject.SetActive(true);
           transitionOutAnimator.Play("TransitionInAnim");
       }
       private void PlayTransitionOutAnim()
       {
           transitionOutAnimator.gameObject.SetActive(true);
           transitionOutAnimator.Play("ExitOptionsAnim");
       }

       private void DisableAllPanels()
       {
           statPanel.SetActive(false);
           creditPanel.gameObject.SetActive(false);
           optionPanel.gameObject.SetActive(false);
       }


    }
}
