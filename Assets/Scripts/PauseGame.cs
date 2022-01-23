using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject menuItems;
    bool isGamePaused = false;
    [SerializeField] HideCursor hideCursor;
    [SerializeField] GameObject endingScreen;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        menuItems.SetActive(isGamePaused);
        hideCursor.enabled = isGamePaused;
        Cursor.visible = isGamePaused;
        if(isGamePaused && !endingScreen.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else if(!isGamePaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    public void BackToMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
