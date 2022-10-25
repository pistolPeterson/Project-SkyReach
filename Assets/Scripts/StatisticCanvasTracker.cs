using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SkyReach
{
    public class StatisticCanvasTracker : MonoBehaviour
    {
        public TextMeshProUGUI JumpNumber;
        private GameObject player;
        private PlayerController con;
        //private GameObject saveBttn;
        private SaveDataSystem saveData;
        // Start is called before the first frame update
        void Start()
        {
            
            //saveBttn = GameObject.Find("Save");
            JumpNumber.text = "0";
        }

        // Update is called once per frame
        void Update()
        {
            //Button saveBttn = tempBttn.GetComponent<Button>();
            player = GameObject.Find("Player");
            con = player.GetComponent<PlayerController>();
            int temp = con.timesJumped;
            JumpNumber.text = temp.ToString();
            //saveBttn.OnClick.AddListener(TaskOnClick);
        }

        // Save Player data once the save button is clicked
        void TaskOnClick()
        {
            saveData = new SaveDataSystem();
            saveData.SaveData();
        }
    }
}

