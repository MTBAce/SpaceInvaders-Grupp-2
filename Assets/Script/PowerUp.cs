using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private AudioClip powerUpClip;

    public PowerUpEffect powerUpEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySoundFXClip(powerUpClip, transform, 1f);
            Destroy(gameObject);
            powerUpEffect.ApplyEffect(collision.gameObject);
        }
       
    }
}
