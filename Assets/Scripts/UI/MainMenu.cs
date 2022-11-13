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


        public void ShowStatPanel()
        {
            statPanel.gameObject.SetActive(true);
        }
     
     
       public void ShowOptionPanel()
       {
           Debug.Log("showing option panel");
            optionPanel.gameObject.SetActive(true);
       }
    }
}
