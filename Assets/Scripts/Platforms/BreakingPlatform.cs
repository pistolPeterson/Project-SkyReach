using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BreakingPlatform : MonoBehaviour
{
    public int timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other){
        Destroy(gameObject,timer);
    }

}
