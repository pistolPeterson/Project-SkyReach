using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;

namespace SkyReach.Enemies.Projectiles
{

    public class Rocket : MonoBehaviour
    {
        [SerializeField] private float speed = 25f;
        [SerializeField] private float lifeTime = 10f;
        [SerializeField] private float maxTurnSpeed = 0.5f;

        private Rigidbody2D _body;
        private Rigidbody2D _target;
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            _body = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _target = GameManager.Player.Body;
            Destroy(gameObject, lifeTime);
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            // rotate 2D towards target
            Vector2 direction = _target.position - _body.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            _body.MoveRotation(Mathf.LerpAngle(_body.rotation, angle, maxTurnSpeed * speed * Time.fixedDeltaTime));

            _body.velocity = transform.up * speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == _target)
            {
                GameManager.KillPlayer();
                Destroy(gameObject);

            }

            // Destroy the rocket if it hit the ground
            // Ground layer is an int of 3
            // It can go through semi-solid objects like the player
            // This assumes all semi-solids are in the same direction, solid facing up
            if (collision.gameObject.layer == 3 && _body.velocity.y < 0)
            {

                //play explosion 
                //wait
                //destroy

                StartCoroutine(RocketExplode());
                //Destroy(gameObject);
            }
        }
        private IEnumerator RocketExplode()
        {
            anim.SetTrigger("Explode");
            yield return new WaitForSeconds(0.3f);
            

            Destroy(gameObject);

        }
    }

}