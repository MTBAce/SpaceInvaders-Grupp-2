using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{
    public abstract void ApplyEffect(GameObject Player);
}
