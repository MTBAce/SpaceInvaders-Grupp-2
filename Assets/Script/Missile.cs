using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Missile : Projectile
{

    ScreenShake screenShake;
    private void Awake()
    {
        direction = Vector3.down;
        screenShake = Camera.main.GetComponent<ScreenShake>();
    }
   
    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); 
        if (collision.gameObject.layer != LayerMask.NameToLayer("Boundary"))
        {
            screenShake.TriggerShake(0.15f, 0.17f);
        }
        
    }
   
}
