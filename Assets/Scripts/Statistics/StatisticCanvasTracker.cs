using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// This class updates UI values based on values found in the StatisticsData class.
/// </summary>
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


        public void SetData()
        {
            
            tempData = playerData.GetJumps();
            JumpNumber.text = tempData.ToString();

            tempData = playerData.GetDeaths();
            PlayerDeaths.text = tempData.ToString();

            tempData = playerData.GetHooks();
            TimesGrappled.text = tempData.ToString();

            tempData = playerData.GetEnemiesKilled();
            EnemiesKilled.text = tempData.ToString();

          
            PersonalBestTime.text = playerData.GetBestRunTime().ToString();
            if (timer == Single.MaxValue)
            {
                PersonalBestTime.text = "--.--";
            } 

           
            
        }
}


