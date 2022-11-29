using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioBase : MonoBehaviour
{
    [SerializeField] private AudioSource source; 
    [SerializeField] private AudioClip actionClip1;


    public void PlayMainActionSfxClip()
    {
        source.PlayOneShot(actionClip1);
    }
}
