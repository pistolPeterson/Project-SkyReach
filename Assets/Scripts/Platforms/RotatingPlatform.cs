using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace SkyReach.Platforms
{
    public class RotatingPlatform : MonoBehaviour
    {
        [SerializeField] private Vector3 startRotation;
        [SerializeField] private int speed;
        [SerializeField] private bool counterClockwise;

        void Start()
        {
            transform.eulerAngles = startRotation;
        }

        void Update()
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime * (counterClockwise ? -1 : 1));
        }
    }
}
