using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkyReach.Enemies.Projectiles;

namespace SkyReach.Enemies
{

    public class ShootingEnemy : MonoBehaviour
    {
    public Vector2 LaunchOffset;
    public float timer;
    public int shootTimer;
    public GameObject Boolet;
    // Start is called before the first frame update
        void Start()
        {
        
        }

    // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if(timer>=shootTimer){
                Instantiate(Boolet,LaunchOffset,transform.rotation);
                timer = 0;
            }

        }
    }
}