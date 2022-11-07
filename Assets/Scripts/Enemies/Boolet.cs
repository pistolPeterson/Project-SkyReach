using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
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
            transform.position += -transform.up * Time.deltaTime * Speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                Destroy(gameObject);
                //play hit animation 
                
                GameManager.Instance.Death();
            }
                
        }
    }
}