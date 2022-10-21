using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatingPlatform : MonoBehaviour
{
    public GameObject rotatingPlatform;
    public Vector3 startRotation;
    public int speed;
    public bool counterClockwise;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = startRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Decides counterclockwise or clockwise rotation
        if (counterClockwise) RotateCounterClockwise();
        else RotateClockwise();

    }

    private void RotateClockwise()
    {

        transform.Rotate(Vector3.back, speed * Time.deltaTime);
    }

    private void RotateCounterClockwise()
    {

        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
