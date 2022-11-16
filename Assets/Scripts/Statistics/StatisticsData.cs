using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class StatisticsData : MonoBehaviour
{
    public static StatisticsData Instance;
    private int jumps = 0;
    private int numHooks = 0;
    private int playerDeath = 0;
    private int enemiesKilled = 0;
    private float bestRunTime = Single.MaxValue;


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

    private void Update()
    {

    }

    public int getJumps()
    {
        return PlayerPrefs.GetInt("Jumps");
    }

    public int getDeaths()
    {
        return PlayerPrefs.GetInt("Deaths"); ;
    }

    public int getHooks()
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
        PlayerController.jump += setJumps;
        GrapplingHook.HookPulled += setHooks;
        GameManager.PlayerDied += setDeaths;

    }

    void OnDisable()
    {
        //unsubscribe 
        PlayerController.jump -= setJumps;
        GrapplingHook.HookPulled -= setHooks;
        GameManager.PlayerDied -= setDeaths;

    }

    public void SetBestRunTime(float latestRunTime)
    {


        if (latestRunTime < PlayerPrefs.GetFloat("BestRunTime"))
            PlayerPrefs.SetFloat("BestRunTime", latestRunTime);

        Debug.Log(PlayerPrefs.GetFloat("BestRunTime") + " is the new best run time");
    }
    void setJumps()
    {
        jumps++;
        PlayerPrefs.SetInt("Jumps", jumps);
        //Debug.Log("Jump triggered!");
    }

    void setDeaths()
    {
        playerDeath++;
        PlayerPrefs.SetInt("Deaths", playerDeath);
    }

    void setHooks()
    {
        numHooks++;
        PlayerPrefs.SetInt("Hooks", numHooks);
        //Debug.Log("Number of times hooked: " + numHooks);
    }

    void setEnemiesKilled()
    {
        enemiesKilled++;
        PlayerPrefs.SetInt("Enemies Killed", enemiesKilled);
    }
}


