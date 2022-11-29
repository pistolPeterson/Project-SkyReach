using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathSFX : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip deathClip;

    public void PlayDeathClip()
    {
        source.PlayOneShot(deathClip);
    }
}
