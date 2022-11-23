using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// return button appears at end credit anim event
/// </summary>
public class EndCreditsAnimation : MonoBehaviour
{
    //reference to button gameobject 
    [SerializeField] private GameObject returnButton;

    [SerializeField] private GameObject endCreditsPanel;
    // Start is called before the first frame update
    void Start()
    {
        //make button not active on start 
           returnButton.SetActive(false);
    }

    public void DisableSelf()
    {
        endCreditsPanel.SetActive(false);
    }
    
    //a method that sets the button gameobject active 
    public void ActivateButton()
    {
        returnButton.SetActive(true);
    }
}
