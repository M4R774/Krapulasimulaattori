using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void quitGame()
    {
        Application.Quit();
    }

}