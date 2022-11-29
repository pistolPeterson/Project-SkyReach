using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
/// <summary>
/// This class keeps track of the player's current and fastest game run time.
/// </summary>
public class Timer : MonoBehaviour
{
    private bool timerActive = false;
    public float currentTime;

    private float finalTime;
    [SerializeField] StatisticsData statsData;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        timerActive = true;
        statsData = FindObjectOfType<StatisticsData>();
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
        if (statsData)
            statsData.IncrementBestRunTime(finalTime);
    }

    public void ResetTimer()
    {
        timerActive = false;
        currentTime = 0;
    }

}


