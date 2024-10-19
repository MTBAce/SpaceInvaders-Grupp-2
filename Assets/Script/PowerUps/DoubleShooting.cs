using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powerups/DoubleShooting")]
public class DoubleShooting : PowerUpEffect
{
    public override void ApplyEffect(GameObject player)
    {
        player.GetComponent<Player>().StartDoubleLaser(5);
        Debug.Log("Double laser powerup applied");
    }
}
