using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject Platform;
    public GameObject BasePlatform;
    public GameObject CeilingPlatform;
    //This is from the scene
    private float startY = 0.0f;
     
    public float heightIncrement = 2.0f;
    public float numOfPlats =6;
    public float platXOffsetMax;
    public float platXOffsetMin;
    public float platHeightIncrementMin;
    public float platHeightIncrementMax;
    // Start is called before the first frame update
    
    void Start(){      
        RandomPlatformGenerator();
    }
    void RandomPlatformGenerator() {
        startY = BasePlatform.gameObject.transform.position.y; 
        //Creates numofPlats number of platforms
        for(int i=0; i< numOfPlats; i++){
        Instantiate(Platform, new Vector3(Random.Range(platXOffsetMin,platXOffsetMax), startY, 0),  Quaternion.identity);
        //Increases vertial spawn point of the next platform
        startY= startY +heightIncrement + Random.Range(platHeightIncrementMin,platHeightIncrementMax);
        }
    }
    // Update is called once per frame
    void Update()
    {   
        //Makes keyboard key K spawn platforms
        if (Input.GetKeyDown(KeyCode.K)){
            RandomPlatformGenerator();
        }
        if (Input.GetKeyDown(KeyCode.L)){
            Instantiate(Platform, new Vector3(Random.Range(platXOffsetMin,platXOffsetMax), Random.Range(startY+heightIncrement, CeilingPlatform.gameObject.transform.position.x), 0),  Quaternion.identity);
        }
    }
}
