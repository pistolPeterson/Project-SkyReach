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
    private GrapplingHook grapplingHook;
    
    private void Start()
    {
        grapplingHook = GetComponent<GrapplingHook>();
    }

    private void Update()
    {
        //splits the battery cooldown into 5 sections and assigns the sprite depending on what current section it is in
        //There are magic numbers, but we hace 5 sprites and the Battery cooldown can be removed
        //from the grappling hook system with no compile errors 
        if (grapplingHook.GetCurrentCooldown() <=  0)
        {
           sr.sprite = batterySpriteLevels[4]; 
        }
        else if (grapplingHook.GetCurrentCooldown() <  (grapplingHook.GetCooldownLength() * (0.2f)))
        {
            sr.sprite = batterySpriteLevels[3]; 
        }
        else if (grapplingHook.GetCurrentCooldown() < (grapplingHook.GetCooldownLength() * (0.4f)))
        {
            sr.sprite = batterySpriteLevels[2];
        }
        else if (grapplingHook.GetCurrentCooldown() < (grapplingHook.GetCooldownLength() * (0.6f)))
        {
            sr.sprite = batterySpriteLevels[1];
        }
        else if (grapplingHook.GetCurrentCooldown() < (grapplingHook.GetCooldownLength() * (0.8f)))
        {
            sr.sprite = batterySpriteLevels[0];
        }
        
        
    }
}
