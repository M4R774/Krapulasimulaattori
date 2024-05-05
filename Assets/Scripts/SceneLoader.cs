using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip menuClickSound;
    
    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene("MainMenu"));
    }
    public void LoadGameplay()
    {
        StartCoroutine(LoadScene("Gameplay"));
    }

    IEnumerator LoadScene(string scene)
    {
        audioSource.PlayOneShot(menuClickSound);
        yield return new WaitForSeconds(1);
        if (scene == "MainMenu") {
            SceneManager.LoadScene("MainMenu");
        }
        else if (scene == "Gameplay") {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
