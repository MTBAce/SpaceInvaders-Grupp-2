using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public AudioClip[] laserShootClip;
    [SerializeField] private AudioClip hurtEffectClip;

    public AudioClip[] playerDeathSound;
    
    public Animator leftFlash;
    public Animator rightFlash;

    public Laser laserPrefab;
    public ScreenShake screenShake;

    GameObject rightLaserMuzzle;
    GameObject leftLaserMuzzle;

    public GameObject LivesPrefab;
    public Transform livesContainer;
    private List<GameObject> lives = new List<GameObject> ();

    private GameManager gameManager;

    Laser laser;


    public int rand;
    bool Powerup2Toggle;
    private int playerLives;

    float laserCoolDown = 0.3f;
    float timeSinceShot = 0f;
    float speed = 10f;

     Vector3 leftCannonOffset = new Vector3(-0.7f, 2.4f, 0);
     Vector3 rightCannonOffset = new Vector3(0.7f, 2.4f, 0);

    private bool isLeftCannon = true;

    private void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
        gameManager = FindObjectOfType<GameManager>();

        playerLives = GameManager.Instance.lives;
        Debug.Log("Player lives:" + playerLives);
        UpdateLives();
    }
   public void Update()
    {
        rand = gameManager.rand;

        //Movemnt for the player
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

        //Shooting for the player, and the powerup2
        if (Input.GetKeyDown(KeyCode.Space) /*|| (Input.GetKeyDown(KeyCode.Mouse0))*/ && Time.time >= timeSinceShot + laserCoolDown)
        {
            Shoot();

            if (Powerup2Toggle == true)
            {
                StartCoroutine(DoubleLaserCoroutine(0));
            }
        }
    }

    //Checks if the player is hit and if it dies 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            playerLives -= 1;
            Debug.Log("Player lives:" + playerLives);
            UpdateLives();

            screenShake.TriggerShake(0.25f, 1.3f);
            SoundManager.instance.PlaySoundFXClip(hurtEffectClip, transform, 1f);
          

            if (playerLives == 0)
            {
                gameManager.OnPlayerKilled(this);

                //DeathSounds for player, it randomizes between the added in unity
                int deathSound = Random.Range(0, playerDeathSound.Length);
                SoundManager.instance.PlaySoundFXClip(playerDeathSound[deathSound], transform, 0.15f);
            }
        }

    }

    public void UpdateLives()
    {
        foreach (GameObject heart in lives)
        {
            Destroy(heart);
        }
        lives.Clear();

        for (int i = 0; i < playerLives; i++)
        {
            GameObject newHeart = Instantiate(LivesPrefab, livesContainer);
            lives.Add(newHeart);
        }
    }
    //Shooting and deciding what part of the ship the shot will come from and sound
    private void Shoot()
    {
        timeSinceShot = Time.time;

        Vector3 laserSpawnPosition = isLeftCannon ? leftCannonOffset : rightCannonOffset;
        laser = Instantiate(laserPrefab, transform.position + laserSpawnPosition, Quaternion.identity);

        if(isLeftCannon == true) 
        {
            leftFlash.SetTrigger("leftFlash");
        }   
        else
        {
            rightFlash.SetTrigger("rightFlash");
        }
        
        isLeftCannon = !isLeftCannon;

        //LaserSounds for the player, it randomizes between the added in unity
        if (laserShootClip.Length >= 1)
        {
            int laserSound = Random.Range(0, laserShootClip.Length);
            SoundManager.instance.PlaySoundFXClip(laserShootClip[laserSound], transform, 0.4f);
        }

        screenShake.TriggerShake(0.12f, 0.35f);

        //Debug.Log("Laser Cooldown before Power-up: " + laserCoolDown);
    }
    //adds a life on collision
    public void AddLife(int amount)
    {
        playerLives += amount;
        Debug.Log("Added life. Current lives: " + playerLives);
        UpdateLives();
    }

    //gives the player faster shooting speed
    public void FastShooting(float duration)
    {
        float startLaserCoolDown = laserCoolDown;

        laserCoolDown *= .4f;
        Debug.Log("Cooldown during power-up:" + laserCoolDown);
        StartCoroutine(ShootCoroutine(duration, startLaserCoolDown));
    }

    //makes the player shoot twice
    public void StartDoubleLaser (float duration)
    {
        StartCoroutine(LaserCoroutine(duration));
        
    }

    //timer for powerups (all below)
    private IEnumerator ShootCoroutine(float duration, float startLaserCoolDown)
    {
        yield return new WaitForSeconds(duration);
        laserCoolDown = startLaserCoolDown;
        //Debug.Log("Cooldown after Power-up:" + laserCoolDown);
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
