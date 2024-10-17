using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public Laser laserPrefab;
    public ScreenShake screenShake;
    
    private GameManager gameManager;

    Laser laser;

    private int playerLives;

    public float laserCoolDown = 0.36f;
    float timeSinceShot = 0f;
    float speed = 8f;


    private void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
        gameManager = FindObjectOfType<GameManager>();


        playerLives = GameManager.Instance.lives;
        Debug.Log("Player lives:" + playerLives);
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
            timeSinceShot = Time.time;
            laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            screenShake.TriggerShake(0.1f, 0.07f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            playerLives -= 1;
            Debug.Log("Player lives:" + playerLives);

 
        }
           
        
    }
}
