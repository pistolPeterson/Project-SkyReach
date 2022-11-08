using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BreakingPlatform : MonoBehaviour
{
    [SerializeField] GameObject breakingPlatform;
    public int timer;
    int layerDefault;
    // Start is called before the first frame update
    void Start()
    {
        layerDefault = LayerMask.NameToLayer("Default");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other){

        //SpriteRenderer.color = new Color(1f,1f,1f,0f);
       breakingPlatform.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
       breakingPlatform.GetComponent<Collider2D>().enabled = false;
        Debug.Log("Current layer: " + breakingPlatform.layer);
    }
}

