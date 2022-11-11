using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using SkyReach.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The game manager, will set the game flow/states and allow the game to pass any data or info.. 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameState currentGameState;
    
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
       
      
        
       
        Level1State();
    }

    public void Level1State()
    {
        currentGameState = GameState.Level1; 

   
    }
    
    public void EndGame() //victory condition
    {
        currentGameState = GameState.EndGame; 
       
        endGame?.Invoke();
      
     
           var lvlChanger = FindObjectOfType<LevelChanger>();
           if(lvlChanger) lvlChanger.FadeToLevel(2); //go to the end credit scene
    }
   

    public void Death()
    {
        
        currentGameState = GameState.Death;
        
        OnPlayerDeath?.Invoke();
        var lvlChanger = FindObjectOfType<LevelChanger>();
        if(lvlChanger) lvlChanger.FadeToLevel(1); //restart level
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