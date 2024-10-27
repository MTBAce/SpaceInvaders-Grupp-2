using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButtons : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    
    public void Credits()
    {
        SceneManager.LoadScene(2); //credits scenen
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game"); //Avslutar build
        Application.Quit();
    }
}
