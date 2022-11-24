using SkyReach.Player;
using UnityEngine;

/// <summary>
/// The audio system for the player itself including the jump, landing sound, stunned/hit sound
/// </summary>
[RequireComponent(typeof(PlayerController), typeof(GrapplingHook))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip groundedSfx;
    [SerializeField] private AudioClip hookBlast;
    [SerializeField] private AudioClip hookRappelSfx;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GrapplingHook hook;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        hook = GetComponent<GrapplingHook>();
    }

    private void OnEnable()
    {
        PlayerController.Landed += PlayGroundedSound;
        PlayerController.Jumped += PlayJumpSFX;
        GrapplingHook.HookFired += PlayHookBlast;
        GrapplingHook.HookPulled += StartHookRappel;
        GrapplingHook.HookFinished += StopHookRappel;
    }

    private void OnDisable()
    {
        PlayerController.Landed -= PlayGroundedSound;
        PlayerController.Jumped -= PlayJumpSFX;
        GrapplingHook.HookFired -= PlayHookBlast;
        GrapplingHook.HookPulled -= StartHookRappel;
        GrapplingHook.HookFinished -= StopHookRappel;
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

    public void StartHookRappel()
    {
        source.clip = hookRappelSfx;
        source.Play();
    }

    public void StopHookRappel()
    {
        source.Stop();
    }
}
