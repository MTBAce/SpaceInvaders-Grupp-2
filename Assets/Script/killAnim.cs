using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillAnim(0.5f)); //Sets a timer for half a second
    }

    IEnumerator KillAnim(float duration)
    {
    yield return new WaitForSeconds(duration);
    Destroy(gameObject); //kills the animation object when the time is met
    }
   
}
