using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    // Spela igen
    public void PlayAgain() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1;

    }
    // Tillbaka till mainmenu
public void mainMenu()
{
        SceneManager.LoadScene("MainMenu");
}


}
