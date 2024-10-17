using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public Laser laserPrefab;
    public ScreenShake screenShake;

    Laser laser;
    

    float laserCoolDown = 0.36f;
    float timeSinceShot = 0f;
    float speed = 8f;



    private void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
    }
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            position.x += speed * Time.deltaTime;
        }

        transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= timeSinceShot + laserCoolDown)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.Instance.OnPlayerKilled(this);
        }

        if (collision.gameObject.CompareTag("Powerup"))
        {
            if (collision.gameObject.name == "Powerup1")
            {
                float cooldown = laserCoolDown;
                laserCoolDown *= 0.5f;
                StartCoroutine(ShootCoroutine(3, cooldown));
            } 
            else if (collision.gameObject.name == "Powerup2")
            {
                float cooldown = laserCoolDown;
                laserCoolDown *= 0.5f;
                Debug.Log(laserCoolDown);
                StartCoroutine(ShootCoroutine(3, cooldown));
            }
            else
            {
                float cooldown = laserCoolDown;
                laserCoolDown *= 0.5f;
                Debug.Log(laserCoolDown);
                StartCoroutine(ShootCoroutine(3, cooldown));
            }
        }
    }

    private void Shoot()
    {
        timeSinceShot = Time.time;
        laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        screenShake.TriggerShake(0.1f, 0.07f);
    }

    private void Powerup1()
    {

    }
    private void Powerup2()
    {

    }
    private void Powerup3()
    {

    }

    IEnumerator ShootCoroutine(float seconds,float cooldown)
    {
        yield return new WaitForSeconds(seconds);
        laserCoolDown = cooldown;
    }
}
