using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
/// <summary>
/// This class is responsible for storing and retrieving data related to the player's performance.
/// </summary>

public class StatisticsData : MonoBehaviour
{
    public static StatisticsData Instance;
    private int jumps = 0;
    private int numHooks = 0;
    private int playerDeath = 0;
    private int enemiesKilled = 0;


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

    }

    public int GetJumps()
    {
        return PlayerPrefs.GetInt("Jumps");
    }

    public int GetDeaths()
    {
        return PlayerPrefs.GetInt("Deaths"); ;
    }

    public int GetHooks()
    {
        return PlayerPrefs.GetInt("Hooks");
    }


    public int GetEnemiesKilled()
    {
        return PlayerPrefs.GetInt("Enemies Killed");
    }

    public float GetBestRunTime()
    {
        return PlayerPrefs.GetFloat("BestRunTime");
    }

    void OnEnable()
    {
        //method that increase jumps
        PlayerController.Jumped += IncrementJumps;
        GrapplingHook.HookPulled += IncrementHooks;
        GameManager.PlayerDied += IncrementDeaths;

    }

    void OnDisable()
    {
        //unsubscribe 
        PlayerController.Jumped -= IncrementJumps;
        GrapplingHook.HookPulled -= IncrementHooks;
        GameManager.PlayerDied -= IncrementDeaths;

    }

    public void IncrementBestRunTime(float latestRunTime)
    {
        if (latestRunTime < PlayerPrefs.GetFloat("BestRunTime"))
            PlayerPrefs.SetFloat("BestRunTime", latestRunTime);
        
        if(PlayerPrefs.GetFloat("BestRunTime") == 0)
            PlayerPrefs.SetFloat("BestRunTime", latestRunTime);

        Debug.Log(PlayerPrefs.GetFloat("BestRunTime") + " is the new best run time");
    }

    void IncrementJumps()
    {
        jumps++;
        PlayerPrefs.SetInt("Jumps", jumps);
    }

    void IncrementDeaths()
    {
        playerDeath++;
        PlayerPrefs.SetInt("Deaths", playerDeath);
    }

    void IncrementHooks()
    {
        numHooks++;
        PlayerPrefs.SetInt("Hooks", numHooks);
    }

    void IncrementEnemiesKilled()
    {
        enemiesKilled++;
        PlayerPrefs.SetInt("Enemies Killed", enemiesKilled);
    }
}


