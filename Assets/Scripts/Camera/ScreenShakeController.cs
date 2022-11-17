using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyReach.Camera
{
    public class ScreenShakeController : MonoBehaviour
    {
        //lets us call ScreenShakeController from anywhere;
        //to call, use:
        //          ScreenShakeController.instance.StartShake(duration, power);
        public static ScreenShakeController instance;

        private float shakeTimeRemaining, shakePower, shakeDissipation, shakeRotation;
        public float rotationMultiplier;


        //final variables 
        private float randomRangeLowerBound = -1f;
        private float randomRangeHigherBound = 1f;

        private float zShakeAmount = 0f;

        private float shakePowerDissipationGoal = 0f;
        private float shakeRotationDissipationGoal = 0f;

        private float xRotationTransform = 0f;
        private float yRotationTransform = 0f;
        //end of final variables


        // Start is called before the first frame update
        void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void LateUpdate()
        {
            if (shakeTimeRemaining > 0)
            {
                shakeTimeRemaining -= Time.deltaTime;


                //how much screen shakes on the x and y axis
                float xShakeAmount = Random.Range(randomRangeLowerBound, randomRangeHigherBound) * shakePower;
                float yShakeAmount = Random.Range(randomRangeLowerBound, randomRangeHigherBound) * shakePower;


                //adds xShakeAmount, yShakeAmount, and zShakeAmount (finalized to 0) to
                //camera's current x, y, z positions respectively
                transform.position += new Vector3(xShakeAmount, yShakeAmount, zShakeAmount);


                //makes shakePower decrease from the current shakePower until the shakePowerDissipationGoal (finalized to 0)
                //at a rate of shakeDissipation * Time.deltaTime
                shakePower = Mathf.MoveTowards(shakePower, shakePowerDissipationGoal, shakeDissipation * Time.deltaTime);


                //makes shakeRotation decrease from the current shakeRotation until the shakeRotationDissipationGoal (finalized to 0)
                //at a rate of shakeDissipation * Time.deltaTime
                shakeRotation = Mathf.MoveTowards(shakeRotation, shakeRotationDissipationGoal,
                                                    shakeDissipation * rotationMultiplier * Time.deltaTime);
            }

            //ensures the camera doesnt have a slight tilt when the shakeRotation ends
            //rotates the screen xRotationTransform  and yRotationTransform degrees (0 and 0) on the x and y axis resoectively
            //rotates the screen shakeRotation * Random.Range(randomRangeLowerBound, randomRangeHigherBound) degrees on the z-axis
            transform.rotation = Quaternion.Euler(xRotationTransform, yRotationTransform, 
                                                    shakeRotation * Random.Range(randomRangeLowerBound, randomRangeHigherBound));

        }


        public void StartShake(float duration, float power)
        {
            Debug.Log("SHAKE");
            shakeTimeRemaining = duration;
            shakePower = power;

            
            shakeDissipation = power / duration;

            shakeRotation = power * rotationMultiplier;

        }
    }
}
