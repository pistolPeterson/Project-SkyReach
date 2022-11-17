using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;

public class FloorDeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.parent.GetComponent<PlayerController>())
        {
            GameManager.KillPlayer();
        }
    }
}
