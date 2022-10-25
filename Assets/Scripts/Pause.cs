using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public bool pauseState = false;
 void StartAndPause()
    {
       if(!pauseState)
       {
            Time.timeScale = 1;
       }
       else 
       {
        Time.timeScale = 0;
       }

    pauseState = !pauseState;
        Debug.Log("The game state is Starting" + pauseState);
    }


    void update() 
    {
        if(input.GetKeyDown("P"))
        {
            StartAndPause();
        }




    }