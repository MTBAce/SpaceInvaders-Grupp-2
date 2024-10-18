using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    public void Credits()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
