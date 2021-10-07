using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Image blackscreen;
    [SerializeField] AudioSource ambientSounds;
    [SerializeField] AudioSource guitarSounds;
    [SerializeField] AudioSource introSounds;
    Coroutine startCoroutine;
    [SerializeField] GameObject globalVolume;
    [SerializeField] Texture mainMenuSkybox;
    [SerializeField] Texture gameplaySkybox;

    public void playGame()
    {
        if (startCoroutine == null)
        {
            startCoroutine = StartCoroutine("StartGame");
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        // TODO: Play audio
        ambientSounds.Stop();
        //guitarSounds.Stop();
        introSounds.Play();

        for (float i = 0; i < 1; i+=0.002f)
        {
            blackscreen.color = new Color(0, 0, 0, i);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Gameplay");
    }
}
