using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyReach.Player {
    public class GravityPlatform : MonoBehaviour
    {
        public float downThrust = 100f;
        private PlayerController player;
        
        
        void Start()
        {
            //private GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            //playerRigidBody = playerObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
        }
        private void OnTriggerEnter(Collider collision) {
        
            if (collision.gameObject.tag == "Player")
            {
                player.Body.AddForce(transform.up * downThrust);
            }
        }
    }
}
