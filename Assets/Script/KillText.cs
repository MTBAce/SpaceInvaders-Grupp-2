using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillText : MonoBehaviour
{
    private float textDuration = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndText(textDuration));
    }

    private IEnumerator EndText(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
