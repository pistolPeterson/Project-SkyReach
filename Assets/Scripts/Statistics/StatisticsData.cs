using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyReach.Player
{
    public class StatisticsData : MonoBehaviour
    {
        public int jumps = 500;
        public int numHooks = 20;
        public int playerDeath = 200;
        public int enemiesKilled = 8;


        public int getJumps()
        {
            return PlayerPrefs.GetInt("Jumps");
        }

        public int getDeaths()
        {
            return PlayerPrefs.GetInt("Deaths"); ;
        }

        public int getHooks()
        {
            return PlayerPrefs.GetInt("Hooks");
        }
        public int getEnemiesKilled()
        {
            return PlayerPrefs.GetInt("Enemies Killed");
        }

        public float getTime()
        {
            return PlayerPrefs.GetFloat("Time");
        }

        void OnEnable()
        {
            //method that increase jumps
            PlayerController.jump += setJumps;

            //method that increases player hooks
            GrapplingHook.hook += setHooks;

            //Personal note: Create event trigger for player deaths

            //Personal note: Create event trigger for enemies killed
        }

        void OnDisable()
        {
            //unsubscribe 
            PlayerController.jump -= setJumps;
            GrapplingHook.hook -= setHooks;
        }

        void setJumps()
        {
            jumps++;
            PlayerPrefs.SetInt("Jumps", jumps);
            //Debug.Log("Jump triggered!");
        }

        void setDeaths()
        {
            playerDeath++;
            PlayerPrefs.SetInt("Deaths", playerDeath);
        }

        void setHooks()
        {
            numHooks++;
            PlayerPrefs.SetInt("Hooks", numHooks);
            //Debug.Log("Number of times hooked: " + numHooks);
        }

        void setEnemiesKilled()
        {
            enemiesKilled++;
            PlayerPrefs.SetInt("Enemies Killed", enemiesKilled);
        }

        /*void setTime(float currentTime)
        {
            PlayerPrefs.setFloat("Time", currentTime);
        }*/
    }
}

