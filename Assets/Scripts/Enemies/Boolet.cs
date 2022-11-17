using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;

namespace SkyReach.Enemies.Projectiles
{

    /// <summary>
    /// A bullet instance script, at spawn will go a specific direction at a specific speed
    /// boolet uwu
    /// </summary>
    public class Boolet : MonoBehaviour
    {
        //speed of the bullet 
        public float Speed = 25f;

        //bullet direction enum, make it easier for designers to set direction of the bullet at spawn if needed
        [SerializeField] private BooletDirection booletDirectionEnum = BooletDirection.Down;


        private Vector3 booletDirection;
        // Start is called before the first frame update
        void Start()
        {
            //default direction is going down
            booletDirection = -transform.up;
            InitBooletDirection();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position += booletDirection * Time.deltaTime * Speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {

                GameManager.KillPlayer();

                Destroy(gameObject);

            }

        }

        //simple switch state to set direction of the bullet based on boolet direction enum
        private void InitBooletDirection()
        {
            switch (booletDirectionEnum)
            {
                case BooletDirection.Up:
                    booletDirection = transform.up;
                    break;
                case BooletDirection.Down:
                    booletDirection = -transform.up;
                    break;
                case BooletDirection.Right:
                    booletDirection = transform.right;
                    break;
                case BooletDirection.Left:
                    booletDirection = -transform.right;
                    break;
            }
        }

        private enum BooletDirection
        {
            Up,
            Down,
            Left,
            Right

        }
    }

}