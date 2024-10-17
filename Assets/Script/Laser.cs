using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Laser : Projectile

{
    float laserSpeed = 18f;
    ScreenShake screenShake;

    private void Awake()
    {
        direction = Vector3.up;
    }


    private void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>(); 
    }

    void Update()
    {
        transform.position += laserSpeed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }

    public void CheckCollision(Collider2D collision)
    {
       Bunker bunker = collision.gameObject.GetComponent<Bunker>();
       screenShake.TriggerShake(0.2f, 0.15f);
        if (bunker == null) 
        {
            Destroy(gameObject);
        }
    }
}
