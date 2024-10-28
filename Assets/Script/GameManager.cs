using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using TMPro;
using Unity.VisualScripting;
using static UnityEditor.FilePathAttribute;
using System;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{

    public int numberOfRounds = 0;
    public AudioClip[] enemyDeathSounds;

    public TextMeshProUGUI scoreText;
    private Music music;

    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;
    private Projectile projectile;

    private PowerUpManager powerupManager;

    public TextMeshProUGUI roundCounter;
    public Canvas canvas;
    public TextMeshProUGUI killScoreText;
    public GameObject GameOverText;
    public bool gameOver = false;
    public int rand;
    public GameObject invaderDeathAnim;
    public GameObject playerDeathAnim;
    public GameObject mysteryShipDeathAnim;

    private int mystershipPoints = 500;
    private int killPoints = 100;
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
        music = FindObjectOfType<Music>();

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

    //restarts the game
    private void NewGame()
    {
        GameOverText.SetActive(false);
        SetScore(0);
        SetLives(3);

        // Reset music speed when starting a new game
        music.speed = 0.95f;
        music.Speed(0.0f); // Reset to base speed, if needed

        numberOfRounds = 0;

        NewRound();

    }


    //starts a new round
    public void NewRound()
    {
        if (invaders == null)
        {
            Debug.LogError("Invaders reference is missing.");
            return;
        }
        if (bunkers == null || bunkers.Length == 0)
        {
            Debug.LogError("Bunkers are missing.");
            return;
        }

        numberOfRounds++;
        Debug.Log(numberOfRounds);
        roundCounter.text = numberOfRounds.ToString();
        // changes colour for the text
        Color currentColor = roundCounter.color;
        currentColor.g = Mathf.Clamp01(currentColor.g - 0.1f);
        currentColor.b = Mathf.Clamp01(currentColor.b - 0.05f);
        currentColor.r = Mathf.Clamp01(currentColor.r - 0.05f);

        roundCounter.color = currentColor;

        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);

        float newInvaderSpeed = invaderSpeed * 1.15f;

        invaderSpeed = newInvaderSpeed;

        Debug.Log("Invader Speed updated: " + invaderSpeed);
        music.Speed(0.05f);
        Debug.Log("New music speed " + music.speed);
    }

    //respawns the player

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

    //ends the game when the player is killed
    public void OnPlayerKilled(Player player)
    {
        GameObject spawnedPlayerDeathAnim = Instantiate(playerDeathAnim, player.gameObject.transform.position, Quaternion.identity);
        player.gameObject.SetActive(false);  
        Debug.Log("Game Over");
        GameOver();

        music.audioSource.Stop();
        music.speed = 0.95f;
    }

    //manages if a powerup should spawn based on a certain amout of kills and what to do when a invader dies (sounds and such)
    public void OnInvaderKilled(Invader invader)
    {
        if (invaderDeathAnim == null)
        {
            Debug.LogError("invaderDeathAnim is not assigned.");
            return;
        }
        GameObject spawnedInvaderDeathAnim = Instantiate(invaderDeathAnim, invader.gameObject.transform.position, Quaternion.identity);
        Animator invaderAnimator = spawnedInvaderDeathAnim.GetComponent<Animator>();

        if (invaderAnimator != null)
        {
            invaderAnimator.SetTrigger("InvaderDeath");
        }
        invader.gameObject.SetActive(false);

        //DeathSounds for enemies, it randomizes between the added in unity
        if (enemyDeathSounds.Length >= 1)
        {
            int deathSound = Random.Range(0, enemyDeathSounds.Length);
            SoundManager.instance.PlaySoundFXClip(enemyDeathSounds[deathSound], transform, 0.2f);
        }

        invaderAnimator.SetTrigger("InvaderDeath");

        kills += 1;
        if (kills >= 10)
        {
           kills = 0;
           powerupManager.SpawnPowerup(invader.gameObject.transform.position);
        }

        SetScore(score + killPoints);

        //makes text saying how much score you get per normal kill
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(invader.transform.position);
        TextMeshProUGUI instantiatedKillScoreText = Instantiate(killScoreText, screenPosition, Quaternion.identity, canvas.transform);
        instantiatedKillScoreText.GetComponent<TextMeshProUGUI>().text = ("+") + killPoints.ToString();

        //new round when all of the invaders are dead

        if (invaders.GetInvaderCount() == 0)
        {
            NewRound();
        }
    }

    //the mysteryships effect on the game
    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        GameObject spawnedMysteryShipDeathAnim = Instantiate(mysteryShipDeathAnim, mysteryShip.transform.position, Quaternion.identity);

        //makes text saying how much score you get per special kill, and change colour and size.

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(mysteryShip.transform.position);
        TextMeshProUGUI instantiatedKillScoreText = Instantiate(killScoreText, screenPosition, Quaternion.identity, canvas.transform);

        instantiatedKillScoreText.GetComponent<TextMeshProUGUI>().text = ("+") + killPoints.ToString();
        SetScore(score + mystershipPoints);
        mysteryShip.gameObject.SetActive(false);

        // set the score text and appearance
        instantiatedKillScoreText.text = ("+") + mystershipPoints.ToString();
        instantiatedKillScoreText.color = Color.yellow;
        instantiatedKillScoreText.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
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
