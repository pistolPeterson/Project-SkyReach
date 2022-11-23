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
        PlayerController.Jumped += SetJumps;
        GrapplingHook.HookPulled += SetHooks;
        GameManager.PlayerDied += SetDeaths;

    }

    void OnDisable()
    {
        //unsubscribe 
        PlayerController.Jumped -= SetJumps;
        GrapplingHook.HookPulled -= SetHooks;
        GameManager.PlayerDied -= SetDeaths;

    }

    public void SetBestRunTime(float latestRunTime)
    {
        if (latestRunTime < PlayerPrefs.GetFloat("BestRunTime"))
            PlayerPrefs.SetFloat("BestRunTime", latestRunTime);

        Debug.Log(PlayerPrefs.GetFloat("BestRunTime") + " is the new best run time");
    }

    void SetJumps()
    {
        jumps++;
        PlayerPrefs.SetInt("Jumps", jumps);
        //Debug.Log("Jump triggered!");
    }

    void SetDeaths()
    {
        playerDeath++;
        PlayerPrefs.SetInt("Deaths", playerDeath);
    }

    void SetHooks()
    {
        numHooks++;
        PlayerPrefs.SetInt("Hooks", numHooks);
        //Debug.Log("Number of times hooked: " + numHooks);
    }

    void SetEnemiesKilled()
    {
        enemiesKilled++;
        PlayerPrefs.SetInt("Enemies Killed", enemiesKilled);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PrefReset();
        }
    }

    public void PrefReset()
    {
        PlayerPrefs.SetInt("Jumps", 0);
        PlayerPrefs.SetInt("Deaths", 0);
        PlayerPrefs.SetInt("Hooks", 0);
        PlayerPrefs.SetInt("Enemies Killed", 0);
        PlayerPrefs.SetFloat("BestRunTime", Single.MaxValue);
    }
}


