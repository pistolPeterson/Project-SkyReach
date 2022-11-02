using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyReach.Player
{
    public class StatisticsData : MonoBehaviour
    {
        public int jumps = 0;
        public int playerDeath = 0;


        public int getJumps()
        {
            return PlayerPrefs.GetInt("Jumps");
        }

        public int getDeaths()
        {
            return PlayerPrefs.GetInt("Deaths"); ;
        }

        void OnEnable()
        {
            //method that increase jumps
            PlayerController.jump += setJumps;
        }

        void OnDisable()
        {
            //unsubscribe 
            PlayerController.jump -= setJumps;
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

        public void loadDeaths(string playDeaths)
        {
            int.TryParse(playDeaths, out playerDeath);
        }

        public void loadJumps(string x)
        {
            int.TryParse(x, out jumps);
        }
    }
}

