using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{   
    //to use shake, add:
    //  CinemachineShake.Instance.ShakeCamera(intensity, duration);
    //to the script where the camera should shake
    public static CinemachineShake Instance;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeDuration;
    private float shakeDurationTotal;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float duration)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        
        startingIntensity = intensity;
        shakeDuration = duration;
    }

    private void Update()
    {
        if (shakeDuration > 0) //if the shake duration hasnt ended
        {
            //count down shake duration
            shakeDuration -= Time.deltaTime;

            if(shakeDuration <= 0f) //if the shake duration is over
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                ////progressively lowers amplitude intensity to 0
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 
                    Mathf.Lerp(startingIntensity, 0f, (1-(shakeDuration / shakeDurationTotal)));
            }
        }
    }
}
