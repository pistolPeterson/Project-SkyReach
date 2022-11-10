using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using SkyReach.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The higher level game manager, will set the game flow/states and allow the game to pass any data or info.. 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameState currentGameState;

    [SerializeField] private Timer timer;
    [SerializeField] private StatisticsData stats;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playerObj;
    public static event Action endGame;
    public static event Action OnPlayerDeath;
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
       Spawn();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

   
    public void Spawn()
    {
        currentGameState = GameState.Spawn; 
        //disable player input 
        playerController.OnDisable();
      
        
       
        Level1State();
    }

    public void Level1State()
    {
        currentGameState = GameState.Level1; 

        //input is back on
        playerController.OnEnable();
        //starting level sfx/UI
        
        //turn timer on 
        timer.StartTimer();
    }
    
    public void EndGame() //victory condition
    {
        Debug.Log("In End game state");
        currentGameState = GameState.EndGame; 
        //stop timer 
        timer.StopTimer();
       
        endGame?.Invoke();
      
        //play end game music
        
        //in a few seconds go to another scene 
            //feedback demo = "thanks fro playing, please give feedback on our game" 
            // main demo = end credits 
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
           var lvlChanger = FindObjectOfType<LevelChanger>();
           if(lvlChanger)
               lvlChanger.FadeToLevel(2);
    }
   

    public void Death()
    {
        if (currentGameState != GameState.Level1) return; 
        //state = death
        currentGameState = GameState.Death;
        
        //start fading out and reset scene 
        
        OnPlayerDeath?.Invoke();
        
        
       var lvlChanger = FindObjectOfType<LevelChanger>();
       if(lvlChanger)
           lvlChanger.FadeToLevel(1);
    }

  

   
   

    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }
}

public enum GameState
{
    Base,
    Spawn,
    Level1,
    Death,
    EndGame
}