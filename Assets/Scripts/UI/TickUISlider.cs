using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TickUISlider : MonoBehaviour
{
    public List<GameObject> ticks;

    private int index = 9;

    [SerializeField] private AudioType audioType;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTicks(); 
    }
    public void IncreaseTick()
    {
        if (index > 9)
        {
            index = 9;
            return;
        }

        index++;
        UpdateTicks();
    }
    
    public void DecreaseTick()
    {
        if (index <= 0)
        {
            index = 0; 
            return;
        }

        index--;
        UpdateTicks();
    }
    
    private void UpdateTicks()
    {
        HideAllTicks();
        for (int i = 0; i < index; i++)
        {
            ticks[i].SetActive(true);
        }

        var audioSystem = FindObjectOfType<AudioMixerSystem>();
        if(!audioSystem) return;

        switch (audioType)
        {
            case AudioType.Music:
                audioSystem.SetMusicVolume(index);
                break; 
            case AudioType.SFX:
                audioSystem.SetSfxVolume(index);
                break;
        }
    }
    
    private void HideAllTicks()
    {
        foreach (var tick in ticks)
        {
            tick.SetActive(false);
        }
    }
}

public enum AudioType
{
    SFX, 
    Music
}
