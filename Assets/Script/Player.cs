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
    
    private GameManager gameManager;

    Laser laser;
    

    private int playerLives;

    float laserCoolDown = 0.36f;
    float timeSinceShot = 0f;
    float speed = 10f;

     Vector3 leftCannonOffset = new Vector3(-1f, 2.4f, 0);
     Vector3 rightCannonOffset = new Vector3(1f, 2.4f, 0);

    private bool isLeftCannon = true;

    private void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
        gameManager = FindObjectOfType<GameManager>();

        playerLives = GameManager.Instance.lives;
        Debug.Log("Player lives:" + playerLives);
    }
   public void Update()
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            playerLives -= 1;
            Debug.Log("Player lives:" + playerLives);

            if (playerLives == 0)
            {
                gameManager.OnPlayerKilled(this);
            }
        }

        if (collision.gameObject.CompareTag("Powerup"))
        {
            if (collision.gameObject.name == "Powerup1")
            {
                float cooldown = laserCoolDown;
                laserCoolDown *= 0.5f;
                //Debug.Log(laserCoolDown);
                StartCoroutine(ShootCoroutine(3, cooldown));
            } 
            else if (collision.gameObject.name == "Powerup2")
            {
                float cooldown = laserCoolDown;
                laserCoolDown *= 0.5f;
                //Debug.Log(laserCoolDown);
                StartCoroutine(ShootCoroutine(3, cooldown));
            }
            else
            {
                float cooldown = laserCoolDown;
                laserCoolDown *= 0.5f;
                //Debug.Log(laserCoolDown);
                StartCoroutine(ShootCoroutine(3, cooldown));
            }
        }
    }

    private void Shoot()
    {
        timeSinceShot = Time.time;

        Vector3 laserSpawnPosition = isLeftCannon ? leftCannonOffset : rightCannonOffset;
        //Debug.Log("Transform.position: " + transform.position);
        laser = Instantiate(laserPrefab, transform.position + laserSpawnPosition, Quaternion.identity);
        //Debug.Log("LaserSpawnPosition: " + laserSpawnPosition);
        //Debug.Log("Transform.position + laserspawnposition: " + transform.position + laserSpawnPosition);
        isLeftCannon = !isLeftCannon;

        screenShake.TriggerShake(0.1f, 0.15f);
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
