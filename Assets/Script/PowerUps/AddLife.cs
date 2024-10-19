using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
[CreateAssetMenu(menuName = "Powerups/ExtraLife")]
public class AddLife : PowerUpEffect
{
    public override void ApplyEffect(GameObject player)
    {
        player.GetComponent<Player>().AddLife(1);
    }
}
