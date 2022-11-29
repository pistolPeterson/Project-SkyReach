using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = SkyReach.Input;

public class Pause : MonoBehaviour, Input.IPauseActions
{
    [SerializeField] private GameObject pausePanel;

    private Input input;

    private bool pauseButtonPressed;

    private bool pausePanelOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(pausePanelOpen);
    }

    private void OnEnable()
    {
        if (input == null)
        {
            input = new Input();
            input.Pause.SetCallbacks(this);
        }

        input.Enable();    
    }


    private void OnDisable()
    {
        input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseButtonPressed)
        {

            pausePanelOpen = !pausePanelOpen;
            pausePanel.SetActive(pausePanelOpen);

            pauseButtonPressed = false; 
        }
        
    }
    
    
    //method to go to main menu 
    
    //method to resume back to game 
    public void OnPauseAction(InputAction.CallbackContext context)
    {

        pauseButtonPressed = context.ReadValueAsButton();
        //  throw new System.NotImplementedException();
    }
}
