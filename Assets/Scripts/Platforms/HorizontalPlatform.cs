using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HorizontalPlatform : MonoBehaviour
{
    [SerializeField] private float platformSpeed = .01f;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(new Vector3(10, transform.position.y, 0), InvertPlatformSpeed()).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float InvertPlatformSpeed()
    {
        return (1/platformSpeed);
    }
}
