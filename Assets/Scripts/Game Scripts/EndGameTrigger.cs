using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public AudioSource source;
    public AudioClip teleportClip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody == GameManager.Player.Body)
        {
            source.PlayOneShot(teleportClip);
            GameManager.WinGame();
        }
    }
}
