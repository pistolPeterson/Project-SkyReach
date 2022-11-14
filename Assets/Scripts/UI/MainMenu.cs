using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SkyReach.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject statPanel;
        
        [SerializeField] private GameObject optionPanel;
        [SerializeField] private GameObject creditPanel;
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ShowMainMenu()
        {
            statPanel.SetActive(false);
            creditPanel.gameObject.SetActive(false);
            optionPanel.gameObject.SetActive(false);
        }
        public void ShowStatPanel()
        {
            creditPanel.gameObject.SetActive(false);
            optionPanel.gameObject.SetActive(false);
            statPanel.gameObject.SetActive(true);
        }
     
     
       public void ShowOptionPanel()
       {
           statPanel.SetActive(false);
            creditPanel.gameObject.SetActive(false);
            optionPanel.gameObject.SetActive(true);

       }
       
       
    }
}
