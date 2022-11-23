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

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        timerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }


    }

    private void OnEnable()
    {
        GameManager.GameWon += StopTimer;

    }

    private void OnDisable()
    {
        GameManager.GameWon -= StopTimer;
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void PauseTimer()
    {
        timerActive = false;
    }

    public void StopTimer()
    {
        timerActive = false;
        finalTime = currentTime;
        var statsData = FindObjectOfType<StatisticsData>();
        if (statsData)
            statsData.SetBestRunTime(finalTime);
    }

    public void ResetTimer()
    {
        timerActive = false;
        currentTime = 0;
    }

}


