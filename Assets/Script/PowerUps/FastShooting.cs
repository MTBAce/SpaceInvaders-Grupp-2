using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powerups/FastShooting")]
public class FastShooting : PowerUpEffect
{
    public override void ApplyEffect(GameObject player)
    {
        player.GetComponent<Player>().FastShooting(5);
        Debug.Log("Fast Shooting powerup applied");
    }
}
