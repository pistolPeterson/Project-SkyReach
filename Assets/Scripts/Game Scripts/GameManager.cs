using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using SkyReach.Player;
using UnityEngine;
/// <summary>
/// The higher level game manager, will set the game flow/states and allow the game to pass any data or info.. 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameState currentGameState;

    [SerializeField] private Timer timer;
    [SerializeField] private StatisticsData stats;
    public static event Action endGame;
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


    public void EndGame() //victory condition
    {
        Debug.Log("In End game state");
        currentGameState = GameState.EndGame; 
        //stop timer 
        timer.StopTimer();
       
        endGame?.Invoke();
        //record time for statistics 
        //disable movement 
        //play end game music
        
        //in a few seconds go to another scene 
            //feedback demo = "thanks fro playing, please give feedback on our game" 
            // main demo = end credits 
    }
    //states 
    //base 
    //FallAnimState
    //lvl 1 
    //death 
    //end game 

    public void FallingDeath()
    {
        //state = death
        //play falling music and sfx 
        //start fading out 
        //reset scene
        //
    }

    public void FallingAnimation()
    {
        //disable player input 
        //play falling animation, player splats into ground 
        //go to lvl 1 state
        
    }

    public void Level1State()
    {
        //input is back on
        //starting sfx/UI
        //turn timer on 
    }

    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }
}

public enum GameState
{
    Base,
    FallAnimationState,
    Level1,
    Death,
    EndGame
}