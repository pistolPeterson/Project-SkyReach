using System;
using System.Collections;
using System.Collections.Generic;
using SkyReach.Player;
using UnityEngine;
/// <summary>
/// The UI script for the grappling hook cooldown
/// </summary>
public class BatteryCooldownUI : MonoBehaviour
{

    [SerializeField] private Sprite[] batterySpriteLevels;//the levels of the battery from empty to full, we are garanteed 5 sprites
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Animator animator;
    private GrapplingHook grapplingHook;

    
    //TODO: Refactor so its going through the 5 sprite levels instead of the 2 I did below 
    private void Start()
    {
        grapplingHook = GetComponent<GrapplingHook>();
    }

    private void Update()
    {
        Debug.Log(grapplingHook.GetCurrentCooldown());

        if (grapplingHook.GetCooldownLength() / 2 > grapplingHook.GetCurrentCooldown()) //if current cooldown is less than half (the timer in grappling hook is counting down)
        {
            sr.sprite = batterySpriteLevels[4]; // show full 
        }
        else
        {
            sr.sprite = batterySpriteLevels[0]; //show empty
        }
    }
}
