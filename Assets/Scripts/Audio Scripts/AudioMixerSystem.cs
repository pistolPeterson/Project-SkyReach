using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerSystem : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] testSfxClips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume(int tickLevel)
    {
        audioMixer.SetFloat("MusicVol", (Mathf.Log10((((tickLevel * 10) + 1.0f) * 0.00000099009901f)) * 40) + 160);
    }
    
    public void SetSfxVolume(int tickLevel)
    {
        audioMixer.SetFloat("SFXVol", (Mathf.Log10((((tickLevel * 10) + 1.0f) * 0.00000099009901f)) * 40) + 160);
        //play test sound here to check audio
        audioSource.clip = testSfxClips[Random.Range(0, testSfxClips.Length)];
        audioSource.Play();
    }
    
}
