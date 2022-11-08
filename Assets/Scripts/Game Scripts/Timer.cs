using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
/// <summary>
/// Add a class summary here 
/// </summary>
public class Timer : MonoBehaviour
{
    private bool timerActive = false;
    public float currentTime;

    private float finalTime;

    //[SerializeField] private StatisticsData playerData;
    
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive == true) 
        {
            currentTime = currentTime + Time.deltaTime;
           
        } 
       
        
    }

        public void StartTimer() 
        {
            timerActive = true;
        }

        public void PauseTimer()
        {
            timerActive = false;
            //playerData.setTime(currentTime);
        }

        public void StopTimer()
        {
            timerActive = false;
            finalTime = currentTime;
            //FindObjectOfType<StatisticsData>().
           
        }
    
        public void ResetTimer()
        {
            timerActive = false;
            currentTime = 0;
        }
        
    }


