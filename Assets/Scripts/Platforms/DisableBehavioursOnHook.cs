using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyReach.Player;

namespace SkyReach.Platforms
{
    public class DisableBehavioursOnHook : MonoBehaviour
    {
        [SerializeField] private List<Behaviour> behaviours;
        private GrapplingHook hook;
        private Rigidbody2D playerBody;

        private void OnTriggerEnter2D(Collision2D other)
        {
            // get hook component
            // hook is on sibling object
            hook = other.transform.parent.GetComponentInChildren<GrapplingHook>();
            
            // enable behaviours if hook is not attached
            if (hook != null && !hook.IsAttached)
            {
                foreach (Behaviour behaviour in behaviours)
                {
                    behaviour.enabled = true;
                }
            }
        }

        private void OnTriggerStay2D(Collision2D other)
        {
            // if hook is attached disable components
            if (hook == other.transform.parent.GetComponentInChildren<GrapplingHook>() && hook.IsAttached)
            {
                foreach (Behaviour behaviour in behaviours)
                {
                    behaviour.enabled = false;
                }
            }
        }

        private void OnTriggerExit2D(Collision2D other)
        {
            // clear hook if it the same as the one that left
            if (other.transform.parent.GetComponentInChildren<GrapplingHook>() == hook)
            {
                hook = null;
            }
        }
    }
}
