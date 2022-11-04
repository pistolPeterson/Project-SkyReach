using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkyReach.Player{
    public class ElectrifiedPlatform : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController; 
        private bool isElectrified;
        private float timer;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            timer+=Time.deltaTime;
            //Debug.Log(timer);
        }

        void onCollisionEnter(Collision collider)
        {
            //put stun here

        }

        void electricityOn()
        {
            isElectrified = true;
        }

        void electricityOff()
        {
            isElectrified = false;
        }

    }
}