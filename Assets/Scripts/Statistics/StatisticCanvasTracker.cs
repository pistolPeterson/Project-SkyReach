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
        public StatisticsData playerData;
        private int temp = 0;
        //private SaveDataSystem saveData;

        // Start is called before the first frame update
        void Start()
        {
            JumpNumber.text = "0";
            if (playerData == null)
            {
                Debug.Log("playerData is null!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            temp = playerData.getJumps();
            JumpNumber.text = temp.ToString();
            //Debug.Log("number of jumps: " + playerData.getJumps());
        }

        /*public void SetData()
        {
            saveData = FindObjectOfType<SaveDataSystem>();
            JumpNumber.text = saveData.LoadData();
            Console.WriteLine("Hello!");
        }*/
    }
}

