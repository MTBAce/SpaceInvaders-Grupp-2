using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    public GameObject Powerup1;
    public GameObject Powerup2;
    public GameObject Powerup3;

    public void SpawnPowerup(Vector3 position)
    {
        int rand = Random.Range(1, 4);
        GameObject powerup = null;

        switch (rand)
        {
            case 1 : powerup = Powerup1; break;
            case 2 : powerup = Powerup2; break;
            case 3 : powerup = Powerup3; break;
        }
   
    if (powerup != null)
        {
            Instantiate(powerup, position, Quaternion.identity);
        }
    
    
    }


   
}
