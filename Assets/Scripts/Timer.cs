using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{   
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    bool timerActive = false;
    float currentTime;
    

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive == true) {
            currentTime = currentTime + Time.deltaTime;
           
        } else if (timerActive == false) {
                currentTime = currentTime;
            }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = time.ToString(@"mm\:ss");
        }

        public void StartTimer() {
            timerActive = true;
        }

        public void StopTimer() {
            timerActive = false;
        }
        
    }


