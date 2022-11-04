using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager) Debug.Log("There should be a gamemanager in the scene!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>(); 
        
        if(player == null || gameManager.GetCurrentGameState() == GameState.EndGame ) return;
       
        GameManager.Instance.EndGame(); 
    }
}
