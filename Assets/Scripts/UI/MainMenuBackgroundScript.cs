using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackgroundScript : MonoBehaviour
{
    [SerializeField] private GameObject backgroundAnimLoadObj;
    [SerializeField] private AudioClip loadUpSfx;
    [SerializeField] private AudioSource _audioSource;
    public void HideBackground()
    {
        backgroundAnimLoadObj.gameObject.SetActive(false);
    }

    public void PlayLoadSFX()
    {
        
        _audioSource.PlayOneShot(loadUpSfx);
    }
    
}
