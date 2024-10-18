using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;
    public GameObject Powerup1;
    public GameObject Powerup2;
    public GameObject Powerup3;


    private float kills = 0;
    public float invaderSpeed { get; private set; } = 0.84f;

    public int score { get; private set; } = 0;
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

        SetScore(0);
        SetLives(3);


        NewRound();

    }

    public void NewRound()
    {
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);

        float newInvaderSpeed = invaderSpeed * 1.8f;
        invaderSpeed = newInvaderSpeed;
        Debug.Log("Invader Speed: " + invaderSpeed);

        for (int i = 0; i < bunkers.Length; i++)
        {
            bunkers[i].ResetBunker();
        }

        Respawn();
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
        invaders.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        
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
        Debug.Log("Disable Invaders");

        kills += 1;
        if (kills >= 1)
        {
            kills = 0;
            int rand = Random.Range(1, 4);
            if (rand == 1)
            {
                Instantiate(Powerup1, invader.gameObject.transform.position, Quaternion.identity);
                Debug.Log(rand);
            }
            if (rand == 2)
            {
                Instantiate(Powerup2, invader.gameObject.transform.position, Quaternion.identity);
                Debug.Log(rand);
            }
            if (rand == 3)
            {
                Instantiate(Powerup3, invader.gameObject.transform.position, Quaternion.identity);
                Debug.Log(rand);
            }
        }

        if (invaders.GetInvaderCount() == 0)
        {
            NewRound();
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        mysteryShip.gameObject.SetActive(false);
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
