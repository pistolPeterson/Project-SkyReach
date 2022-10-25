using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// This script is attached to the platforms that move vertically.
/// </summary>
public class VerticalMovement : MonoBehaviour
{
    [SerializeField][Range(0, 3)] private float platformSpeed = 2f;
    [SerializeField][Range(0, 10)] private float height = 2f;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(new Vector3(transform.position.x, height, 0), InvertSpeed()).SetLoops(-1, LoopType.Yoyo);
    }

    /*
     The DOMove's second paramenter is the cycleLength(speed)
     The higher the cycleLength the slower the speed
     
     So we need to invert the speed to get the desired result
     */
    private float InvertSpeed()
    {
        return 1 / platformSpeed;
    }
}
