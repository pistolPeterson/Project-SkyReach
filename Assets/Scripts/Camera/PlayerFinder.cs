using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFinder : MonoBehaviour
{
    private CinemachineVirtualCameraBase virtualCamera;
    private GameObject player;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCameraBase>();
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                virtualCamera.Follow = player.transform;
                virtualCamera.LookAt = player.transform;
            }
        }
    }
}
