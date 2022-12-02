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
        public float cooldown; // time between deactivation and next activation
        public float electrifiedTime; // time the platform is electrified for


        private float timer = 0;
        private Animator anim;
        private ElectricityState state;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            Electrify();
        }

        private void Update()
        {
            timer += Time.deltaTime;

            switch (state)
            {
                case ElectricityState.UnElectrified:
                    if (timer >= cooldown)
                    {
                        Electrify();
                    }
                    break;
                case ElectricityState.Electrified:
                    if (timer >= electrifiedTime)
                    {
                        UnElectrify();
                    }
                    break;
            }
        }

        private void UnElectrify()
        {
            state = ElectricityState.UnElectrified;
            timer = 0;
            anim.Play("ElectrifiedPlatform_Idle");
        }

        private void Electrify()
        {
            state = ElectricityState.Electrified;
            timer = 0;
            anim.Play("ElectrifiedPlatform_Active");
        }
    }

    public enum ElectricityState //enum to get the states of the electric platform 
    {
        Electrified,
        UnElectrified
    }
}