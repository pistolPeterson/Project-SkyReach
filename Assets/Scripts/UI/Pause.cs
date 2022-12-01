using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Input = SkyReach.Input;
using UnityEngine.Audio;

public class Pause : MonoBehaviour, Input.IPauseActions
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private float audioTransitionTime = 2f;
    [SerializeField] private AudioMixerSnapshot normal; 
    [SerializeField] private AudioMixerSnapshot paused; 
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

           TogglePanel();
            pauseButtonPressed = false; 
        }
        
    }

    public void TogglePanel()
    {
        pausePanelOpen = !pausePanelOpen;
        pausePanel.SetActive(pausePanelOpen);
        if (pausePanelOpen)
        {
            paused.TransitionTo(audioTransitionTime * 0.0001f);
            Debug.Log("hello");

        }
        else 
            normal.TransitionTo(audioTransitionTime  * 0.0001f);

        Time.timeScale = pausePanelOpen ? 0.0001f : 1;
       
    }
    
    //method to go to main menu 
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    //method to resume back to game 
    public void OnPauseAction(InputAction.CallbackContext context)
    {
        pauseButtonPressed = context.ReadValueAsButton();
        //  throw new System.NotImplementedException();
    }
}
