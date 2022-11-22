using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOutAnimation : MonoBehaviour
{

    [SerializeField] AudioSource source; 
    [SerializeField] AudioClip powerupClip; 
    [SerializeField] AudioClip powerdownClip; 
    public void DisableSelf()
    {
        this.gameObject.SetActive(false);
    }
    public void PlayPowerUp()
    {
        source.PlayOneShot(powerupClip);
    }
    
    public void PlayPowerDown()
    {
            source.PlayOneShot(powerdownClip);
    }
    
}
