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

    private double fullCD, halfCD, thirdCD, fourthCD;

    
    //TODO: Refactor so its going through the 5 sprite levels instead of the 2 I did below 
    private void Start()
    {
        grapplingHook = GetComponent<GrapplingHook>();
        fullCD = grapplingHook.GetCooldownLength();
        halfCD = grapplingHook.GetCooldownLength() / 2;
        thirdCD = grapplingHook.GetCooldownLength() / 3;
        fourthCD = grapplingHook.GetCooldownLength() / 4;

    }

    private void Update()
    {
        Debug.Log(fullCD + " " + halfCD + " " + thirdCD + " " + fourthCD + " ");
        Debug.Log(grapplingHook.GetCurrentCooldown());

        // Must keep cooldownLength at 6 for this to look good in the game:
        if (grapplingHook.GetCurrentCooldown() < fourthCD && grapplingHook.GetCurrentCooldown() > 0)
            sr.sprite = batterySpriteLevels[4]; // full; 4 ticks >>> animator.Play("BatteryAnim");
        else if (grapplingHook.GetCurrentCooldown() < thirdCD && grapplingHook.GetCurrentCooldown() > fourthCD)
            sr.sprite = batterySpriteLevels[3]; // 3 ticks
        else if (grapplingHook.GetCurrentCooldown() < halfCD && grapplingHook.GetCurrentCooldown() > thirdCD)
            sr.sprite = batterySpriteLevels[2]; // 2 ticks
        else if (grapplingHook.GetCurrentCooldown() < fullCD && grapplingHook.GetCurrentCooldown() > halfCD)
            sr.sprite = batterySpriteLevels[1]; // 1 tick
        else if (grapplingHook.GetCurrentCooldown() >= fullCD)
            sr.sprite = batterySpriteLevels[0]; // empty
        else if (grapplingHook.GetCurrentCooldown() <= 0)
            sr.sprite = null; // full (not showing)


        /* if (grapplingHook.GetCooldownLength() / 2 > grapplingHook.GetCurrentCooldown()) 
         //if current cooldown is less than half (the timer in grappling hook is counting down)
         {
             sr.sprite = batterySpriteLevels[4]; // show full 
         }
         else
         {
             sr.sprite = batterySpriteLevels[0]; //show empty
         }*/
    }
}
