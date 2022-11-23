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
        public float Speed = 25f;
        private float lifeTime = 3.33f;
        private Rigidbody2D _body;

        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            Destroy(this.gameObject, lifeTime);
        }

        void FixedUpdate()
        {
            _body.velocity = transform.up * Speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponentInParent<PlayerController>())
            {
                GameManager.KillPlayer();
                Destroy(gameObject);
            }

            if (collision.gameObject.layer == 3 && _body.velocity.y < 0)
            {
                Destroy(gameObject);
            }

        }
    }

}