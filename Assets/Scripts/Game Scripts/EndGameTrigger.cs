using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.GetComponent<PlayerController>() == null) return;

        Debug.Log("winning game");
        GameManager.WinGame();
    }
}
