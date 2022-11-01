using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles animations
namespace SkyReach.Player 
{
    public class AnimationScript : MonoBehaviour
    {
        Animator animator;
        private string currentState;
        const string playerIdleRight = "PlayerIdleRight";
        const string playerIdleLeft = "PlayerIdleLeft";
        const string playerRunRight = "PlayerRunRight";
        const string playerRunLeft = "PlayerRunLeft";
        const string playerJumpingRight = "PlayerJumpingRight";
        const string playerJumpingLeft = "PlayerJumpingLeft";
        [SerializeField] private PlayerController playerOne;
        private float facing;
        private Vector2 zeroVector = new Vector2(0,0);

        void Start()
        {
            animator = GetComponent<Animator>();        
        }

        void Update()
        {
            //Gets the player facing from the player controller
            facing = playerOne.LastHorizontalFacingDirection.x;        
            
                //Plays walk right animation when player is moving right
                if (facing > 0) 
                {
                    //Plays idle animation if the player is not moving, if the player is moving plays the appropriate facing animation
                    if (!playerOne.IsGrounded())
                    {
                       //ChangeAnimationState(playerJumpingRight); 
                       Debug.Log("right jumping anim plays here"); 
                    }
                    else if (playerOne.FacingDirection == zeroVector) 
                    {                   
                        ChangeAnimationState(playerIdleRight);
                    }
                    else 
                    {                  
                        ChangeAnimationState(playerRunRight);
                    }
                    
                }
                else if (facing < 0){
                   
                   if (!playerOne.IsGrounded())
                    {
                       //ChangeAnimationState(playerJumpingLeft); 
                       Debug.Log("left jumping anim plays here"); 
                    }                   
                    else if (playerOne.FacingDirection == zeroVector) 
                    {                        
                        ChangeAnimationState(playerIdleLeft);                    
                    }
                    else
                    {
                        ChangeAnimationState(playerRunLeft);
                    }
                }    
        }
                
        void ChangeAnimationState(string newState)
        {
            //stops the same animation from interrupting itself
            if (currentState == newState) return;
            //play animation
            animator.Play(newState);

            //reassign the current state
            currentState = newState;
        }       
    }
 }
