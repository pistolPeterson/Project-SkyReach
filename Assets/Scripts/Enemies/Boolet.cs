using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyReach.Enemies.Projectiles
{


    public class Boolet : MonoBehaviour
    {
        public float Speed = 4f;

    // Start is called before the first frame update
        void Start()
        {

        }

    // Update is called once per frame
        void Update()
        {
         transform.position += -transform.right * Time.deltaTime * Speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}