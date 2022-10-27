using UnityEngine;
using System.Collections.Generic;

namespace SkyReach
{
    public class MassMultiplierEffector : MonoBehaviour
    {
        [SerializeField] private float massMultiplier = 1f;

        private List<Rigidbody2D> bodies = new List<Rigidbody2D>();

        public void OnDisable()
        {
            // reset mass
            foreach (Rigidbody2D rigidbody in bodies)
            {
                rigidbody.mass /= massMultiplier;
            }
            // clear list
            bodies.Clear();
        }

        // on collision enter, multiply mass of attached rigidbody
        public void OnTriggerEnter2D(Collider2D other)
        {
            // only accept collisions on colliders with usedByEffector true
            if (!other.usedByEffector) return;
            // get rigidbody
            Rigidbody2D otherBody = other.gameObject.GetComponent<Rigidbody2D>();
            if (otherBody != null)
            {
                // add to list
                bodies.Add(otherBody);
                // multiply mass
                otherBody.mass *= massMultiplier;
            }
        }

        // on collision exit, divide mass of attached rigidbody
        public void OnCollisionExit2D(Collision2D collision)
        {
            // only accept collisions on colliders with usedByEffector true
            if (!collision.otherCollider.usedByEffector) return;

            // get rigidbody
            Rigidbody2D otherBody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherBody != null && bodies.Contains(otherBody))
            {
                // remove from list
                bodies.Remove(otherBody);
                // divide mass
                otherBody.mass /= massMultiplier;
            }
        }
    }
}