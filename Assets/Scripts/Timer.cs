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
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    private bool timerActive = false;
    private float currentTime;

    private float finalTime;  
    
    //TODO Reset timer method 
    //Add a stop button, similar to how I did the start button
    //add a reset timer UI button
    //add a pause method. (what would be difference between stopping the time and pausing the time?)
    

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
        else
        {
           currentTime = currentTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = time.ToString(@"mm\:ss");
    }

        public void StartTimer() {
            timerActive = true;
        }

        public void StopTimer() {
            timerActive = false;
        }
        
    }


