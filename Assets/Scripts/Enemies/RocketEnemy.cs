using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyReach.Enemies.Projectiles;

namespace SkyReach.Enemies
{

    public class RocketEnemy : MonoBehaviour
    {
        [SerializeField] private float activationRadius = 25f;
        [SerializeField] private GameObject rocketPrefab;
        [SerializeField] private float timeBetweenShots = 5f;

        private float timer = 0;

        void Update()
        {
            if (timer > 0) timer -= Time.deltaTime;
            else if (Vector2.Distance(transform.position, GameManager.Player.Body.position) < activationRadius)
            {
                timer = timeBetweenShots;
                ShootRocket();
            }
        }

        void ShootRocket()
        {
            Instantiate(rocketPrefab, transform.position, Quaternion.identity);
        }
    }
}