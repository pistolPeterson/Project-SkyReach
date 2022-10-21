using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyReach.Camera
{
    public class ScreenShakeController : MonoBehaviour
    {
        //lets us call ScreenShakeController from anywhere
        public static ScreenShakeController instance;

        private float shakeTimeRemaining, shakePower, shakeDissipation, shakeRotation;

        public float rotationMultiplier;

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
                float xShakeAmount = Random.Range(-1f, 1f) * shakePower;
                float yShakeAmount = Random.Range(-1f, 1f) * shakePower;

                //moves camera depending on x and y shake amount to create the shake effect
                transform.position += new Vector3(xShakeAmount, yShakeAmount, 0f);

                shakePower = Mathf.MoveTowards(shakePower, 0f, shakeDissipation * Time.deltaTime);

                shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeDissipation * rotationMultiplier * Time.deltaTime);
            }

            transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));

        }


        public void StartShake(float duration, float power)
        {
            shakeTimeRemaining = duration;
            shakePower = power;

            shakeDissipation = power / duration;

            shakeRotation = power * rotationMultiplier;

        }
    }
}
