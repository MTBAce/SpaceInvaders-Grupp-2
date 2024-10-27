using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Elliott
public class ScreenShake : MonoBehaviour
{

    //Variables to control the length and how intense the screenshake should be
    float shakeMagnitude = 0;
    float shakeDuration = 0;
    float dampingSpeed = 1.0f;

    private Vector3 initialPosition; //Cameras inital position
    private float currentShakeDuration; //Keeps track how long the screen has shaked


    void Start()
    {
        initialPosition = transform.position; //Sets inital position
    }


    void Update()
    {
        if(currentShakeDuration > 0 )
        {
            Vector3 shakeOffset = Random.insideUnitCircle * shakeMagnitude;

            transform.localPosition = initialPosition + shakeOffset;

            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            currentShakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }


    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;    // How long the shake should last
        shakeMagnitude = magnitude;  // The strength of the shake
        currentShakeDuration = shakeDuration; 
    }
}
