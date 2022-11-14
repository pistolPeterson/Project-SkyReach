using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsUI : MonoBehaviour
{

    [SerializeField] private GameObject screenShakeImg;

    private bool screenShakeOn = true;
    // Start is called before the first frame update
    void Start()
    {
        screenShakeImg.SetActive(screenShakeOn);
    }

   
    public void ToggleScreenShake()
    {
        Debug.Log("pressed screen shake!");
        screenShakeOn = !screenShakeOn;
        screenShakeImg.SetActive(screenShakeOn);
    }
    
}
