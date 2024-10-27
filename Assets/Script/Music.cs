using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class Music : MonoBehaviour
{
    public float speed = 0.85f;
    public AudioClip music;
    private AudioSource audioSource;

    void Awake()
    {
        // Initialize audioSource early to ensure its ready before other scripts call Speed()
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("no audio component found");
        }
    }

    void Start()
    {
        // Doublecheck if audioSource is there
        if (audioSource == null)
        {
            Debug.LogError("no audio component");
            return;
        }

        audioSource.clip = music;
        audioSource.volume = 0.1f;
        audioSource.pitch = speed;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Is called to change speed when a new wave spawns
    public void Speed(float increase)
    {
        // incase audiosource is not set up
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                Debug.LogError("audioSource is still not set in speed method");
                return;
            }
        }

        speed += increase;
        speed = Mathf.Clamp(speed, 0.1f, 3.0f);
        audioSource.pitch = speed;
    }
}