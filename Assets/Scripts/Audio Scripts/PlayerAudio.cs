using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using Unity.VisualScripting;
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
    [SerializeField] private AudioClip hookRappelSfx;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GrapplingHook hook;
    private bool lastFrameIsGrounded = false;

    private Input input;

    private bool playingGrappleAudio = false;
    public HookAudioState hookState = HookAudioState.Base;


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
            PlayGroundedSound();
            //Debug.Log("playing landing sound ");
            lastFrameIsGrounded = true;
        }
        else
        {//else add the result to the bool array, with circular index 
            lastFrameIsGrounded = playerController.IsGrounded();

        }

        HookAudioStateMachine();
        if (hook.State == GrapplingHook.HookState.Pulling && !playerController.IsGrounded() && !playingGrappleAudio)
        {
            playingGrappleAudio = true;
        }
        else
        {
            playingGrappleAudio = false;
        }


    }

    private void HookAudioStateMachine()
    {
        switch (hookState)
        {
            case HookAudioState.Base:
                if (hook.State == GrapplingHook.HookState.Pulling && !playerController.IsGrounded() && !playingGrappleAudio)
                {
                    hookState = HookAudioState.StartRapel;
                }
                break;
            case HookAudioState.StartRapel:
                source.clip = hookRappelSfx;
                source.Play();
                hookState = HookAudioState.Rappeling;
                break;
            case HookAudioState.Rappeling:
                if (playerController.IsGrounded())
                {
                    source.Stop();

                    hookState = HookAudioState.Base;
                }
                break;

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
        PlayerController.Jumped += PlayJumpSFX;
    }

    private void OnDisable()
    {
        PlayerController.Jumped -= PlayJumpSFX;
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
    public void PlayGroundedSound()
    {
        source.PlayOneShot(groundedSfx);
    }

    public void PlayHookRappel()
    {
        source.PlayOneShot(hookRappelSfx);
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

public enum HookAudioState
{
    StartRapel,
    Rappeling,
    StopRappel,
    Base,
}
