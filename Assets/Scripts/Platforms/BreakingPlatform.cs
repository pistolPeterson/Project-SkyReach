using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SkyReach.Player;

public class BreakingPlatform : MonoBehaviour
{
    [SerializeField] GameObject breakingPlatform;
    [SerializeField] float respawnTimer = 2.0f;
    [SerializeField] float turnOffTimer = 1.5f; 
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Turns platform off on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        //this verifies that the object that is colliding is the player obj
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController == null) return;
        
        //Turns platform off
        StartCoroutine(TogglePlatformOff());
    }
    //Turns platform off, calls the turn platform on coroutine
    IEnumerator TogglePlatformOff()
    {
        yield return new WaitForSeconds(turnOffTimer);
        breakingPlatform.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
        breakingPlatform.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(TogglePlatformOn());

    }
    //Turns platform on 
    IEnumerator TogglePlatformOn()
    {
        yield return new WaitForSeconds(respawnTimer);
        breakingPlatform.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = true;
        breakingPlatform.GetComponent<Collider2D>().enabled = true;
       
    }
}
