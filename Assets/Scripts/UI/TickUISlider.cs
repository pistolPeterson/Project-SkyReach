using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TickUISlider : MonoBehaviour
{
    public List<GameObject> ticks;

    private int index = 9;
    private bool isHeldDown = false;
    private float timer = 0;
    private float changeTickTime = 0.18f;

    [SerializeField] private AudioType audioType;

    [SerializeField] private TickSliderType tickSliderType;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTicks(); 
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!isHeldDown) return;

        if (timer > changeTickTime)
        {
            if(tickSliderType == TickSliderType.Increase)
                IncreaseTick();
            else 
                DecreaseTick();

            timer = 0;
        }
        
    }

    public void OnPress ()
    {
        isHeldDown = true;
    }
 
    public void OnRelease ()
    {
        isHeldDown = false;
    }

    public void IncreaseButtonIdentify(bool isIncreaseButton)
    {
        if (isIncreaseButton)
            tickSliderType = TickSliderType.Increase;
        else
            tickSliderType = TickSliderType.Decrease;
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

public enum TickSliderType
{
    Increase, 
    Decrease
}
