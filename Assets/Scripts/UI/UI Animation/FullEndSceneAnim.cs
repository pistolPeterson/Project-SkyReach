using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullEndSceneAnim : MonoBehaviour
{
    public void GoToMainMenu()
    {
        var lvlChange = FindObjectOfType<FadeToBlack>();
        if (lvlChange)
            lvlChange.FadeToLevel(0);

    }
    
}
