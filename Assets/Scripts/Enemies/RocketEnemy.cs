using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyReach.Enemies.Projectiles;

namespace SkyReach.Enemies
{

    public class RocketEnemy : MonoBehaviour
    {
        [SerializeField] private Collider2D activationCollider;
        [SerializeField] private GameObject rocketPrefab;
        [SerializeField] private float timeBetweenShots = 5f;
        private Animator anim;

        private float timer = 0;
        private Rigidbody2D _body;


        void Awake()
        {
            anim = GetComponent<Animator>();
            _body = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (timer > 0) timer -= Time.deltaTime;
            else if (GameManager.Player.Body.IsTouching(activationCollider))
            {
                timer = timeBetweenShots;
                ShootRocket();
            }
        }

        void ShootRocket()
        {
            anim.Play("RocketEnemy_SHOOT");
            Instantiate(rocketPrefab, _body.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 180));
        }
    }
}