using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using SkyReach.Player;
using UnityEngine;

namespace Platforms
{
    public class ElectrifiedPlatform : MonoBehaviour
    {
        public float electricityStateTime = 5f; //change electricity state this amount of seconds
        private PlayerStun playerStun;
      

        private float timer = 0;
        private Animator anim; 
        public ElectricityState state; //public for debugging purposes
        
        
        
        // Start is called before the first frame update
        private void Start()
        {
            playerStun = FindObjectOfType<PlayerStun>();
            anim = GetComponent<Animator>();
            
            if (playerStun == null) ;
                Debug.Log("No player stun system in Scene!");
                
            state = ElectricityState.UnElectrified;
            anim.enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
            timer+=Time.deltaTime;

            if (!(timer > electricityStateTime)) return;
            
            switch (state)
            {
                case ElectricityState.UnElectrified:
                    state = ElectricityState.TransitionToElectrified; 
                    break;
                    
                case ElectricityState.TransitionToElectrified:
                    //play electric platform animation
                    //play electric platform sfx
                    anim.enabled = true;
                    anim.Play("ElectrifiedPlatformAnim");
                

                    timer = 0; 
                    state = ElectricityState.Electrified;
                    break; 
                    
                case ElectricityState.Electrified:
                    state = ElectricityState.TransitionToUnElectrified;
                    break;
                    
                case ElectricityState.TransitionToUnElectrified:
                    //stop play playing animation 
                    //play anim sfx
                    anim.enabled = false;
                    timer = 0; 
                    state = ElectricityState.UnElectrified;
                       
                    break;
                default:
                    break;

            }



        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            //this verifies that the object that is colliding is the player obj
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController == null) return;
            
            //if state is electrified 
            //call playerStun.StunPlayer()
            if (state == ElectricityState.Electrified)
            {
                //freeze player 
                
                //stun animation 
                //playerStun.StunPlayer();
                
                //invoke death (//fade in fade out, and reset scene) 
                GameManager.Instance.Death();
            }
            
            
        }

       

    }
    
    public enum ElectricityState //enum to get the states of the electric platform 
    {
        Electrified, 
        UnElectrified, 
        TransitionToElectrified, 
        TransitionToUnElectrified
    }
}