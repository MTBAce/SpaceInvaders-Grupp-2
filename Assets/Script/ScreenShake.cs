using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    float shakeMagnitude = 0;
    float shakeDuration = 0;
    float dampingSpeed = 1.0f;

    private Vector3 initialPosition;
    private float currentShakeDuration;


    void Start()
    {
        initialPosition = transform.position;
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
        shakeDuration = duration;    // Assign the duration parameter to shakeDuration
        shakeMagnitude = magnitude;  // Assign the magnitude parameter to shakeMagnitude
        currentShakeDuration = shakeDuration;  // Initialize the currentShakeDuration to start shaking
    }
}
