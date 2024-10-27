using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerRepair : MonoBehaviour
{
    public GameObject[] Bunkers = new GameObject[4];
    public Vector2[] spawnPositions = new Vector2[4];

    public string bunkerTag = "Bunker";
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            DeleteBunkers();//Tar bort existerande bunkrar
           
            if (Bunkers.Length == 4 && spawnPositions.Length == 4) //bunkrar spawnar på en viss position när du plockar upp poweruppen
            {

                for (int i = 0; i < Bunkers.Length; i++)
                {
                    Instantiate(Bunkers[i], spawnPositions[i], Quaternion.identity);
                }
            }
            else
            {
                Debug.LogError("Please assign exactly 4 objects and 4 spawn positions in the Inspector.");
            }
            Destroy(gameObject);
        }
    }
    private void DeleteBunkers()
    {
        GameObject[] existingBunkers = GameObject.FindGameObjectsWithTag(bunkerTag);
        foreach (GameObject bunker in existingBunkers) 
        {
            Destroy(bunker); 
        }
    }
}
