using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public static bool isCutsceneOn;
    public Animator cameraAnimator;
    public float cutsceneDuration;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the cutscene trigger object collides with the player
        if (collision.tag == "Player")
        {
            isCutsceneOn = true;
            cameraAnimator.SetBool("cutscene1", true);
            Invoke(nameof(StopCutscene), cutsceneDuration);
        }
    }

    void StopCutscene()
    {
        isCutsceneOn = false;
        cameraAnimator.SetBool("cutscene1", false);
    }
}
