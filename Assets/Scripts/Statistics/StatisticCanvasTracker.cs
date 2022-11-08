using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace SkyReach.Player
{
    public class StatisticCanvasTracker : MonoBehaviour
    {
        public TextMeshProUGUI JumpNumber;
        public TextMeshProUGUI PlayerDeaths;
        public TextMeshProUGUI PersonalBestTime;
        public TextMeshProUGUI EnemiesKilled;
        public TextMeshProUGUI TimesGrappled;
        public StatisticsData playerData;
        private int temp = 0;
        private float timer = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            JumpNumber.text = "0";
            PlayerDeaths.text = "0";
            PersonalBestTime.text = "0.00";
            EnemiesKilled.text = "0";
            TimesGrappled.text = "0";
            if (playerData == null)
            {
                Debug.Log("playerData is null!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            //event is fired off that triggers SetData method
        }

        public void SetData()
        {
            /*saveData = FindObjectOfType<SaveDataSystem>();
            JumpNumber.text = saveData.LoadData();
            Console.WriteLine("Hello!");*/
            temp = playerData.getJumps();
            JumpNumber.text = temp.ToString();

            temp = playerData.getDeaths();
            PlayerDeaths.text = temp.ToString();

            temp = playerData.getHooks();
            TimesGrappled.text = temp.ToString();

            temp = playerData.getEnemiesKilled();
            EnemiesKilled.text = temp.ToString();

            /*timer = playerData.getTime();
            PersonalBestTime.text = timer.ToString();*/
        }
    }
}

