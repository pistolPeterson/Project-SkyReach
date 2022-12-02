using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void Update() { } // empty update method enables the script to be enabled/disabled, do not remove
    private void OnTriggerStay2D(Collider2D col)
    {
        if (enabled && col.attachedRigidbody == GameManager.Player.Body)
        {
            GameManager.KillPlayer();
        }
    }
}
