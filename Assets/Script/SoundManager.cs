using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using JetBrains.Annotations;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static SoundManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
   
        audioSource.clip = audioClip;

        audioSource.Play();

        audioSource.volume = volume;
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    
    }
}








