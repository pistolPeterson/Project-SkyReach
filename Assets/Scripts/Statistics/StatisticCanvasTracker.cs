using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


    public class StatisticCanvasTracker : MonoBehaviour
    {
        public TextMeshProUGUI JumpNumber;
        public TextMeshProUGUI PlayerDeaths;
        public TextMeshProUGUI PersonalBestTime;
        public TextMeshProUGUI EnemiesKilled;
        public TextMeshProUGUI TimesGrappled;
        public StatisticsData playerData;
        private int tempData = 0;
        private float timer = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
           
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
            tempData = playerData.getJumps();
            JumpNumber.text = tempData.ToString();

            tempData = playerData.getDeaths();
            PlayerDeaths.text = tempData.ToString();

            tempData = playerData.getHooks();
            TimesGrappled.text = tempData.ToString();

            tempData = playerData.GetEnemiesKilled();
            EnemiesKilled.text = tempData.ToString();

            timer = playerData.GetBestRunTime();
            Debug.Log(timer);
            PersonalBestTime.text = timer.ToString();
        }
    }


