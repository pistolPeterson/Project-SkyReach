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
            statPanel.SetActive(false);
            creditPanel.gameObject.SetActive(false);
            optionPanel.gameObject.SetActive(false);
            PlayTransitionOutAnim();
        }
        public void ShowStatPanel()
        {
            creditPanel.gameObject.SetActive(false);
            optionPanel.gameObject.SetActive(false);
            statPanel.gameObject.SetActive(true);
            statsUI.SetData();
            PlayTransitionOutAnim();
        }
     
     
       public void ShowOptionPanel()
       {
           statPanel.SetActive(false);
            creditPanel.gameObject.SetActive(false);
            optionPanel.gameObject.SetActive(true);
            PlayTransitionOutAnim();

       }

       public void PlayTransitionOutAnim()
       {
           transitionOutAnimator.gameObject.SetActive(true);
           transitionOutAnimator.Play("ExitOptionsAnim");
       }


    }
}
