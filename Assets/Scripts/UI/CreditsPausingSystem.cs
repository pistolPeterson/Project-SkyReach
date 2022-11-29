using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for pausing and resuming the animation for the game's end credits.
/// </summary>

public class CreditsPausingSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && animator.speed == 1)
        {
            animator.speed = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && animator.speed == 0)
        {
            animator.speed = 1;
        }
    }
}
