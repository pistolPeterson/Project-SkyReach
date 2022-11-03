using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;
/// <summary>
/// This system is about stunning trigger and animation.
/// </summary>

public class PlayerStun : MonoBehaviour
{
    [SerializeField]private Animator animator;

    private PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        StunPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StunPlayer()
    {
        
        //play stun animation 
        animator.Play("StunAnimation");
        //disable movement 
        controller.OnDisable();
        //Die = restart level, play death music, play stun sfx 
    }
}
