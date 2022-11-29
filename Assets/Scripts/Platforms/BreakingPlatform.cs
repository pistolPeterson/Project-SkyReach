using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SkyReach.Player;

public class BreakingPlatform : MonoBehaviour
{
    [SerializeField] float respawnTimer = 2.0f;
    [SerializeField] float turnOffTimer = 1.5f;

    [SerializeField] private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Turns platform off on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this verifies that the object that is colliding is the player obj
        PlayerController playerController = collision.collider.GetComponentInParent<PlayerController>();
        if (playerController == null) return;

        //Turns platform off
        StartCoroutine(TogglePlatformOff());
    }
    //Turns platform off, calls the turn platform on coroutine
    IEnumerator TogglePlatformOff()
    {
        //call animation 
        anim.SetTrigger("PlatformGlitchTrigger");
        yield return new WaitForSeconds(turnOffTimer);
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(TogglePlatformOn());

    }
    //Turns platform on 
    IEnumerator TogglePlatformOn()
    {
        yield return new WaitForSeconds(respawnTimer);
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

    }
}
