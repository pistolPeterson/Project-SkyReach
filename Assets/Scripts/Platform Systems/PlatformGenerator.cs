using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;
    public GameObject basePlatform;
    //This is from the scene
    private float startY = 0.0f;
    public List<GameObject> listOfPlatforms = new List<GameObject>();
    public int i =0;
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
        startY = basePlatform.gameObject.transform.position.y+heightIncrement; 
        //Creates numofPlats number of platforms
        for(int i=0; i< numOfPlats; i++){
            //GameObject p = ReturnPlatform();

            var pObj = Instantiate(ReturnPlatform(), new Vector3(Random.Range(platXOffsetMin,platXOffsetMax), startY, 0),  Quaternion.identity);
        //Increases vertial spawn point of the next platform
        startY= startY +heightIncrement + Random.Range(platHeightIncrementMin,platHeightIncrementMax);


            if(pObj != null){
                 Debug.Log(i);
                pObj.GetComponent<BasePlatform>().Spawn();
            }
            else
                Debug.Log("no pobj");

        }
    }
    //Returns random platform from a list
    GameObject ReturnPlatform() {
        return listOfPlatforms[Random.Range(0, listOfPlatforms.Count)];
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
