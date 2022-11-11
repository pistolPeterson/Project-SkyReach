using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackgroundScript : MonoBehaviour
{
    [SerializeField] private GameObject backgroundAnimLoadObj;
    public void HideBackground()
    {
        backgroundAnimLoadObj.gameObject.SetActive(false);
    }

}
