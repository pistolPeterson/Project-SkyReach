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
       
    }

    public void PlayJumpSfx()
    {
        RandomizeAudio();
        audioSource.PlayOneShot(jumpSfx);
    }

    private void RandomizeAudio()
    {
        audioSource.pitch = Random.Range(0.975f, 1.250f);
        audioSource.volume = Random.Range(0.95f, 1.0f);
    }
}
