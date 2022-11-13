using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickUISlider : MonoBehaviour
{
    public List<GameObject> ticks;

    private int index = 5;
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
    }
    
    private void HideAllTicks()
    {
        foreach (var tick in ticks)
        {
            tick.SetActive(false);
        }
    }
}
