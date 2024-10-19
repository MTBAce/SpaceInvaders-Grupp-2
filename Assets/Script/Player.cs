using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip laserShootClip;
    [SerializeField] private AudioClip hurtEffectClip;
    
    
    public Laser laserPrefab;
    public ScreenShake screenShake;
    
    private GameManager gameManager;

    Laser laser;


    public int rand;
    bool Powerup2Toggle;
    private int playerLives;

    float laserCoolDown = 0.3f;
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
        rand = gameManager.rand;


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

        if (Input.GetKeyDown(KeyCode.Space) /*|| (Input.GetKeyDown(KeyCode.Mouse0))*/ && Time.time >= timeSinceShot + laserCoolDown)
        {
            Shoot();

            if (Powerup2Toggle == true)
            {
                StartCoroutine(DoubleLaserCoroutine(0));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            playerLives -= 1;
            Debug.Log("Player lives:" + playerLives);

            screenShake.TriggerShake(0.25f, 1.3f);
            SoundManager.instance.PlaySoundFXClip(hurtEffectClip, transform, 1f);

            if (playerLives == 0)
            {
                gameManager.OnPlayerKilled(this);
            }
        }

    }

    private void Shoot()
    {
        timeSinceShot = Time.time;

        Vector3 laserSpawnPosition = isLeftCannon ? leftCannonOffset : rightCannonOffset;
        laser = Instantiate(laserPrefab, transform.position + laserSpawnPosition, Quaternion.identity);

        isLeftCannon = !isLeftCannon;
        SoundManager.instance.PlaySoundFXClip(laserShootClip, transform, 0.5f);
    
        screenShake.TriggerShake(0.12f, 0.35f);

        Debug.Log("Laser Cooldown before Power-up: " + laserCoolDown);
    }

    public void AddLife(int amount)
    {


        playerLives += amount;
        Debug.Log("Added life. Current lives: " + playerLives);
    }

    public void FastShooting(float duration)
    {
        float startLaserCoolDown = laserCoolDown;

        laserCoolDown *= .4f;
        Debug.Log("Cooldown during power-up:" + laserCoolDown);
        StartCoroutine(ShootCoroutine(duration, startLaserCoolDown));
    }

    public void StartDoubleLaser (float duration)
    {
        StartCoroutine(LaserCoroutine(duration));
        
    }


    private IEnumerator ShootCoroutine(float duration, float startLaserCoolDown)
    {
        yield return new WaitForSeconds(duration);
        laserCoolDown = startLaserCoolDown;
        Debug.Log("Cooldown after Power-up:" + laserCoolDown);
    }

    private IEnumerator LaserCoroutine(float duration)
    {
        Powerup2Toggle = true;
        yield return new WaitForSeconds(duration);
        Powerup2Toggle = false;
    }

    IEnumerator DoubleLaserCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Shoot();
    }

}
