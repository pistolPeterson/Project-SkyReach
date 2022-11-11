using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip jumpSfx;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerController.jump += PlayJumpSFX;
    }

    private void OnDisable()
    {
        PlayerController.jump -= PlayJumpSFX;
    }
    public void PlayJumpSFX()
    {
        source.PlayOneShot(jumpSfx);
    }
}