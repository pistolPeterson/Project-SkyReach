using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    public GameObject Platform;
    public GameObject BasePlatform; //This is from the scene   
    private float startY = 0.0f;    
    public float heightIncrement;
    public float numOfPlats;
    public float platXOffsetMax;
    public float platXOffsetMin;
    
    // Start is called before the first frame update   
    void Start(){      
        RandomPlatformGenerator();
    }

    //Randomly spawns platforms spanning left to right
    void RandomPlatformGenerator() {
        
        startY = BasePlatform.gameObject.transform.position.y;  
        //Generates numOfPlats number of platforms
        for(int i=0; i< numOfPlats; i++){
            Instantiate(Platform, new Vector3(Random.Range(platXOffsetMin,platXOffsetMax), startY, 0),  Quaternion.identity);
            //Sets the amount of space between platforms by + heightIncrement
            startY+=heightIncrement;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
