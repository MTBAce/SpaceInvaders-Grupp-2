using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Laser : Projectile

{

    public ScreenShake screenShake;

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
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }

    void CheckCollision(Collider2D collision)
    {
      Destroy(gameObject);
    

      screenShake.TriggerShake(0.2f, 0.15f);


    }
}
