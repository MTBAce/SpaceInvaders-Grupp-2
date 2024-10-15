using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Missile : Projectile
{

    public ScreenShake screenShake;
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
        Destroy(gameObject); //så fort den krockar med något så ska den försvinna.

        screenShake.TriggerShake(0.1f, 0.15f);
    }
   
}
