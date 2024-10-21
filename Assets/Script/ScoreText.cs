using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void IncrementalCountCount(int scoreTotal)
    {
        scoreText.text = $"Score: {scoreTotal}";
    }
}
