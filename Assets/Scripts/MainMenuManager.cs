using System.IO;
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

    [Header("Platform specific settings")]
    [SerializeField] GameObject quitOptions;

    [Header("Game Settings")]
    [SerializeField] List<GameObject> strobeYesNo;
    private bool areWhiteStrobesEnabled = true;

    [Header("Menu sounds")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip menuClickSound;

    void Start()
    {
        #if UNITY_WEBGL
            quitOptions.SetActive(false);
        #endif
        CheckSettings();
    }

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
        //introSounds.Play();

        for (float i = 0; i < 1; i+=0.002f)
        {
            blackscreen.color = new Color(0, 0, 0, i);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Gameplay");
    }
    void CheckSettings()
    {
        GameSettings gameSettings;
        if(Directory.Exists(Application.persistentDataPath))
        {
            string settingsPath = Application.persistentDataPath;
            DirectoryInfo directoryInfo = new DirectoryInfo(settingsPath);
            foreach (var file in directoryInfo.GetFiles("*.json"))
            {
                StreamReader reader = file.OpenText();
                gameSettings = JsonUtility.FromJson<GameSettings>(reader.ReadToEnd());
                if(gameSettings.areWhiteStrobesEnabled)
                {
                    areWhiteStrobesEnabled = true;
                    strobeYesNo[0].SetActive(false);
                    strobeYesNo[1].SetActive(true);
                }
                else if(!gameSettings.areWhiteStrobesEnabled)
                {
                    areWhiteStrobesEnabled = false;
                    strobeYesNo[0].SetActive(true);
                    strobeYesNo[1].SetActive(false);
                }
            }
        }

    }

    public void ToggleWhiteStrobing()
    {  
        if(areWhiteStrobesEnabled)
        {
            areWhiteStrobesEnabled = false;
            strobeYesNo[0].SetActive(true);
            strobeYesNo[1].SetActive(false);

            GameSettings gameSettings = new GameSettings();
            gameSettings.areWhiteStrobesEnabled = false;
            string gameSettingsString = JsonUtility.ToJson(gameSettings);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/GameSettings.json", gameSettingsString);
        }
        else if(!areWhiteStrobesEnabled)
        {
            areWhiteStrobesEnabled = true;
            strobeYesNo[0].SetActive(false);
            strobeYesNo[1].SetActive(true);

            GameSettings gameSettings = new GameSettings();
            gameSettings.areWhiteStrobesEnabled = true;
            string gameSettingsString = JsonUtility.ToJson(gameSettings);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/GameSettings.json", gameSettingsString);
        }
    }

    public void PlayMenuClickSound()
    {
        audioSource.PlayOneShot(menuClickSound);
    }
}
