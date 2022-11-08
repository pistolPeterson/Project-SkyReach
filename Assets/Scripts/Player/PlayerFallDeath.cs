using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Death system. two types of deaths
/// 1. player getting out of bounds based on height
/// 2. "insta" death called by obstacles/enemies 
/// </summary>
public class PlayerFallDeath : MonoBehaviour
{
    [SerializeField] private Transform playerLocation; 
    private float currentHeight = 0; //current height always set to 0 by default, to be init of where the player is spawned

    [SerializeField] private float deathHeightDistance;//the distance below currentHeight that will enable a "death" 

    private float currentDeathHeight;

    // Start is called before the first frame update
    void Start()
    {
        currentHeight = playerLocation.position.y; 
        currentDeathHeight = currentHeight - deathHeightDistance; 
    }

    // Update is called once per frame
    void Update()
    {
        CalculateHeights();
    }

    private void CalculateHeights()
    {
        currentHeight = playerLocation.position.y; 
        
        var tempDeathHeight = currentHeight - deathHeightDistance;
        
        if (tempDeathHeight > currentDeathHeight)
            currentDeathHeight = tempDeathHeight;

        CheckDeath(); 
    }

    private void CheckDeath()
    {
        if (currentHeight < currentDeathHeight)
        {
            //freeze player at that point
            //play glitch animation
           //call death ( //fade in fade out, and reset scene) 
           GameManager.Instance.Death();
      
        }
           
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        var pointA = new Vector3(-10, currentDeathHeight, 0);
        var pointB = new Vector3(10, currentDeathHeight, 0);
        Gizmos.DrawLine(pointA, pointB);
    }
}
