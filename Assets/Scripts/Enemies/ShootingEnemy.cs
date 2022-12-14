using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyReach.Enemies.Projectiles;

namespace SkyReach.Enemies
{

    public class ShootingEnemy : MonoBehaviour
    {

        public float timer;
        public float shootTimer;
        public GameObject Boolet;

        private Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= shootTimer)
            {
                anim.Play("ProjectileEnemy_Attack", -1, 0f);
                timer = 0;
            }

        }


        public void ShootBullet()
        {
            Instantiate(Boolet, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 180));
        }

    }
}