using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
/// <summary>
/// The higher level game manager, will set the game flow/states and allow the game to pass any data or info.. 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameState currentGameState;

    [SerializeField] private Timer timer; 
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentGameState = GameState.Base; 
        //start timer
        timer.StartTimer();
        //transition to 'base' level music state if needed 

    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    
    //states 
    //base 
    //lvl 1 
    //death 
    //end game 
}

public enum GameState
{
    Base,
    Level1,
    Death,
    EndGame
}