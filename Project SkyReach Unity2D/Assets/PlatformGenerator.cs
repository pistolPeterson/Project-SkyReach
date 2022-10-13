using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    public GameObject Platform;
    public GameObject BasePlatform;
    //This is from the scene
    private float startY = 0.0f;
     
    public float heightIncrement = 2.0f;
    public float numOfPlats =6;
    public float platXOffsetMax;
    public float platXOffsetMin;
    // Start is called before the first frame update
    
    void Start(){      
        RandomPlatformGenerator();
    }
    void RandomPlatformGenerator() {
        startY = BasePlatform.gameObject.transform.position.y; 
        for(int i=0; i< numOfPlats; i++){
        Instantiate(Platform, new Vector3(Random.Range(platXOffsetMin,platXOffsetMax), startY, 0),  Quaternion.identity);
        startY+=heightIncrement;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
