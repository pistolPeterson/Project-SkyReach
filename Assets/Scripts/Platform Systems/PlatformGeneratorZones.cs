using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneratorZones :MonoBehaviour
{
    public GameObject Platform;
    public GameObject BasePlatform;
    public GameObject CeilingPlatform; 
    //This is from the scene
     
    //public float heightIncrement = 2.0f;
    private int numOfRegions =8;
    //Hold the left(Min) and right(Max) screen boundaries
    public float platXOffsetMax;
    public float platXOffsetMin;
    public float platYOffsetMin;
    public float platYOffsetMax;
    //Determines the horizontal space between regions
    private float regionInterval_X = 5.0f;
    //Allows for the turning on or off of platform generation regions
    private bool[] platZones; 
    public bool botLeft = true; //corresponds to 0
    public bool botCenterL = true;
    public bool botCenterR = true;
    public bool botRight = true;
    public bool topLeft = true;
    public bool topCenterL = true;
    public bool topCenterR = true;
    public bool topRight = true; //corresponds to 7
    private float minXBound;
    private float maxXBound;
          
    // Start is called before the first frame update
    
    void Start(){      
        RandomPlatformGenerator();
    }
    void RandomPlatformGenerator() {
        //Divides the stage into eight zones, creates a platform for each zone that set to true
        platZones = new bool[numOfRegions];
        platZones[0] = botLeft;
        platZones[1] =botCenterL;
        platZones[2] =botCenterR;         
        platZones[3] =botRight;
        platZones[4] =topLeft;
        platZones[5] =topCenterL;
        platZones[6] =topCenterR;
        platZones[7] =topRight;  

        minXBound = -10.0f;
        maxXBound = -5.0f;
        //Creates a platform in each of six regions
        for(int i=0; i< numOfRegions; i++){
            //Sets the boundary for the Y coordinates, to create the bottom half
            if ( i <= 3){
            platYOffsetMin = BasePlatform.gameObject.transform.position.y; 

            }
            if (i ==4){
                //resets coordinates to generate the top half
                platYOffsetMin = 0.0f;
                platYOffsetMax = CeilingPlatform.gameObject.transform.position.y;
                minXBound = platXOffsetMin;
                maxXBound = platXOffsetMax;
                }
 
            //Generates a platform only if the region boolean is set to true
            if (platZones[i]) {
                Instantiate(Platform, new Vector3(Random.Range(minXBound,maxXBound), Random.Range(platYOffsetMin, platYOffsetMax), 0),  Quaternion.identity);  
            }
            minXBound +=regionInterval_X;
            maxXBound +=regionInterval_X;
        }
    }
    // Update is called once per frame
    void Update()
    {   
        //Makes keyboard key K spawn platforms
        if (Input.GetKeyDown(KeyCode.K)){
            RandomPlatformGenerator();
        }

    }
}
