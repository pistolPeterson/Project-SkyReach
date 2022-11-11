using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = SkyReach.Input;

/// <summary>
/// The audio system for the player itself including the jump, landing sound, stunned/hit sound
/// </summary>
public class PlayerAudio : MonoBehaviour, Input.IHookActions
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip groundedSfx;
    [SerializeField] private AudioClip hookBlast;
    [SerializeField] private PlayerController playerController;
    private bool lastFrameIsGrounded = false;
    
    private Input input;
 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.IsGrounded() && !lastFrameIsGrounded && playerController.Body.velocity.y <= 20.0f) 
        {
            //play land sound
            source.PlayOneShot(groundedSfx);
            //Debug.Log("playing landing sound ");
            lastFrameIsGrounded = true;
        }
        else
        {//else add the result to the bool array, with circular index 
            lastFrameIsGrounded = playerController.IsGrounded();
           
        }
    }

    private void OnEnable()
    {
        if (input == null)
        {
            input = new Input();
            input.Hook.SetCallbacks(this);
        }
        input.Enable();
        PlayerController.jump += PlayJumpSFX;
    }

    private void OnDisable()
    {
        PlayerController.jump -= PlayJumpSFX;
        input.Disable();
    }
    public void PlayJumpSFX()
    {
        source.PlayOneShot(jumpSfx);
    }

    public void PlayHookBlast()
    {
        source.PlayOneShot(hookBlast);
    }
    public void PlayGroundedSounded()
    {
        source.PlayOneShot(groundedSfx);
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayHookBlast();
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }
}
