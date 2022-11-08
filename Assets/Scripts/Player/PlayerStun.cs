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
    

    public void StunPlayer() //will be refactored to have variable stun time depending on severity of damage
    {
        if (animator == null)
        {
            Debug.Log("Animator is null!");

            return; 
        }
        
        animator.Play("StunAnimation");
       
        
    }
}
