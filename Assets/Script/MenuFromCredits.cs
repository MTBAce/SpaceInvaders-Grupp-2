using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFromCredits : MonoBehaviour
{
    // Start is called before the first frame update
    
    

    // Update is called once per frame
    public void SwitchScene(string MainMenu)
    {
        SceneManager.LoadScene(MainMenu);
    }
}
