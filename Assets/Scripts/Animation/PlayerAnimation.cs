using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the player animations. 
namespace SkyReach.Player 
{
    public class PlayerAnimation : MonoBehaviour
    {
        private string currentState;
        private float facing;
        const string playerIdleRight = "PlayerIdleRight";
        const string playerIdleLeft = "PlayerIdleLeft";
        const string playerRunRight = "PlayerRunRight";
        const string playerRunLeft = "PlayerRunLeft";
        const string playerJumpingRight = "PlayerJumpingRight";
        const string playerJumpingLeft = "PlayerJumpingLeft";
        const string playerFallLeft = "PlayerFallLeft";
        const string playerFallRight = "PlayerFallRight";


        [SerializeField] private Animator animator;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float fallSpeedThreshold = -20.1f;
        private Vector2 zeroVector = new Vector2(0,0);
        private bool isFalling = false;

        void Update()
        {
            CheckIfFalling();
            ControlAnimation();
        }

        void ControlAnimation()
        {
            //Gets the player facing from the player controller
            facing = playerController.LastHorizontalFacingDirection.x;


            if (isFalling)
            {
                if (facing > 0)
                {
                    ChangeAnimationState(playerFallRight);
                }
                else
                {
                    ChangeAnimationState(playerFallLeft);
                }
                return;
            }
            
            
                //Plays walk right animation when player is moving right
                if (facing > 0) 
                {
                    //Plays idle animation if the player is not moving, if the player is moving plays the appropriate facing animation
                    if (!playerController.IsGrounded)
                    {
                       ChangeAnimationState(playerJumpingRight); 
                    }
                    else if (playerController.FacingDirection == zeroVector) 
                    {                   
                        ChangeAnimationState(playerIdleRight);
                    }
                    else 
                    {                  
                        ChangeAnimationState(playerRunRight);
                    }
                    
                }
                else if (facing < 0){
                   
                   if (!playerController.IsGrounded)
                    {
                       ChangeAnimationState(playerJumpingLeft); 
                    }                   
                    else if (playerController.FacingDirection == zeroVector) 
                    {                        
                        ChangeAnimationState(playerIdleLeft);                    
                    }
                    else
                    {
                        ChangeAnimationState(playerRunLeft);
                    }
                }                
        }

        private void CheckIfFalling()
        {
            if (playerController.Body.velocity.y <= fallSpeedThreshold)
            {
                isFalling = true;
            }
            else
            {
                isFalling = false;
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
