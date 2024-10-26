using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float deathTimer = 0;

    [SerializeField] private AudioClip powerUpClip;

    public PowerUpEffect powerUpEffect;

    //adds effect on collision with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySoundFXClip(powerUpClip, transform, 1f);
            Destroy(gameObject);
            powerUpEffect.ApplyEffect(collision.gameObject);
        }
       
    }
    //destroys the powerup after some time to not have it fall forever
    private void Update()
    {
        deathTimer += Time.deltaTime;
        if (deathTimer == 5)
        {
            Destroy(gameObject);
        }
    }
}
