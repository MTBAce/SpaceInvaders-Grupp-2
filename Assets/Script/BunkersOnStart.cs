using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-2)]

public class BunkersOnStart : MonoBehaviour
{
    public GameObject[] Bunkers = new GameObject[4];
    public Vector2[]spawnPositions = new Vector2[4];

    void Start()
    {
        //spawnar bunkrar
        InstantiateBunkers();
    }

    private void InstantiateBunkers()
    {
        if(Bunkers.Length == 4 && spawnPositions.Length == 4)
        {
            for (int i = 0; i < Bunkers.Length; i++)
            {
                Instantiate(Bunkers[i], spawnPositions[i], Quaternion.identity);
            }

        }
        else
        {
            Debug.LogError("Not enough Bunkers Assigned");
        }
    }
}
