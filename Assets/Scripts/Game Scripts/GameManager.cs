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
    public GameState CurrentGameState;
    
    public static event Action endGame;
    public static event Action OnPlayerDeath;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    

    public void EndGame() //victory condition
    {
        CurrentGameState = GameState.EndGame; 
       
        endGame?.Invoke();
      
     
           var lvlChanger = FindObjectOfType<LevelChanger>();
           if(lvlChanger) lvlChanger.FadeToLevel(2); //go to the end credit scene
    }
   

    public void Death()
    {
        
        CurrentGameState = GameState.Death;
        
        OnPlayerDeath?.Invoke();
        var lvlChanger = FindObjectOfType<LevelChanger>();
        if(lvlChanger) lvlChanger.FadeToLevel(1); //restart level
    }
    

}

public enum GameState
{
    Death,
    EndGame
}