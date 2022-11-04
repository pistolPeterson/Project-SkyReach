using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyReach.Player
{
    public class StatisticsData : MonoBehaviour
    {
        public int jumps = 0;
        public int numHooks = 0;
        public int playerDeath = 0;


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

        void OnEnable()
        {
            //method that increase jumps
            PlayerController.jump += setJumps;
            GrapplingHook.hook += setHooks;
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
            Debug.Log("Jump triggered!");
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
            Debug.Log("Number of times hooked: " + numHooks);
        }
    }
}

