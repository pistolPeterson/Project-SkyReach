using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOutAnimation : MonoBehaviour
{
    [SerializeField] private AudioClip powerDownSfx;
    [SerializeField] private AudioClip powerUpSfx;
    [SerializeField] private AudioSource source; 
    public void DisableSelf()
    {
        this.gameObject.SetActive(false);
    }


    public void PlayPowerDownSfx()
    {
        source.PlayOneShot(powerDownSfx);
    }
    
    public void PlayPowerUpSfx()
    {
        source.PlayOneShot(powerUpSfx);
    }
}
