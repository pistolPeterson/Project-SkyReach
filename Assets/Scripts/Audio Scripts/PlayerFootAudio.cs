using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;

public class PlayerFootAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip[] walkClipsSfx;

    [SerializeField] private AudioClip jumpSfx;

    [SerializeField] private float footstepRate = 0.33f;
    private PlayerController playerController; 

    private float footstepCooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (!playerController)
        {
            Debug.Log("Player controller not in scene!");
        } 
    }

    // Update is called once per frame
    void Update()
    {
        footstepCooldown -= Time.deltaTime;
        if ((playerController.FacingDirection.x != 0) && footstepCooldown < 0f && playerController.IsGrounded())
        {
            audioSource.PlayOneShot(walkClipsSfx[0]);
            footstepCooldown = footstepRate;
        }
    }

    public void PlayJumpSfx()
    {
        Debug.Log("is we jumping tho?");
        audioSource.PlayOneShot(jumpSfx);
    }
}
