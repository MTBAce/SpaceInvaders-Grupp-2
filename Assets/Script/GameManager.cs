using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using TMPro;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public AudioClip[] enemyDeathSounds;

    public TextMeshProUGUI scoreText;

    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;

    private PowerUpManager powerupManager; 

    public GameObject GameOverText;
    public bool gameOver = false;
    public int rand;
    public GameObject invaderDeathAnim;

    private float kills = 0;
    public float invaderSpeed { get; private set; } = 0.8f;

    public int score { get; private set; } = 0000000;
    public int lives { get; private set; } = 3;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
        mysteryShip = FindObjectOfType<MysteryShip>();
        bunkers = FindObjectsOfType<Bunker>();

        powerupManager = FindObjectOfType<PowerUpManager>();
     

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        GameOverText.SetActive(false);
        SetScore(0);
        SetLives(3);


        NewRound();

    }

    public void NewRound()
    {
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);

        float newInvaderSpeed = invaderSpeed * 1.7f;
        invaderSpeed = newInvaderSpeed;
        Debug.Log("Invader Speed: " + invaderSpeed);

        for (int i = 0; i < bunkers.Length; i++)
        {
            bunkers[i].ResetBunker();
        }


    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        //invaders.gameObject.SetActive(false);
        mysteryShip.gameObject.SetActive(false);
        GameOverText.SetActive(true);
        gameOver = true;
    }

    private void SetScore(int newScore)
    {
      score = newScore;
      scoreText.text = score.ToString("D6");
    }

    private void SetLives(int lives)
    {

    }

    public void OnPlayerKilled(Player player)
    {
        player.gameObject.SetActive(false);  
        Debug.Log("Game Over");
        GameOver();

    }

    public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);
        
   
        GameObject spawnedDeathAnim = Instantiate(invaderDeathAnim, invader.gameObject.transform.position, Quaternion.identity);

        //DeathSounds for enemies, it randomizes between the added in unity
        int deathSound = Random.Range(0, enemyDeathSounds.Length);
        SoundManager.instance.PlaySoundFXClip(enemyDeathSounds[deathSound], transform, 0.2f);

        Animator invaderAnimator = spawnedDeathAnim.GetComponent<Animator>();
        invaderAnimator.SetTrigger("InvaderDeath");

        kills += 1;
        if (kills >= 10)
        {
           kills = 0;
           powerupManager.SpawnPowerup(invader.gameObject.transform.position);
        }

        SetScore(score + 100);
            
        if (invaders.GetInvaderCount() == 0)
        {
            NewRound();
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        mysteryShip.gameObject.SetActive(false);
        SetScore(score + 500);
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);
            Debug.Log("Disable Invaders, boundary");
            OnPlayerKilled(player);
        }
    }

}
