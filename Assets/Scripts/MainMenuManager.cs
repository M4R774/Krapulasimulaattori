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
    public GameSettings currentGameSettings;
    [SerializeField] List<GameObject> strobeYesNo;
    private bool areWhiteStrobesEnabled;
    public float lookSensitivity;

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
            // if there is not save game file
            if( directoryInfo.GetFiles("*.json").Length == 0)
            {
                Debug.Log("No saved game settings found. Creating one with default settings.");
                gameSettings = new GameSettings();
                gameSettings.areWhiteStrobesEnabled = false;
                areWhiteStrobesEnabled = true;
                strobeYesNo[0].SetActive(true);
                gameSettings.lookSensitivity = 2.5f;
                string gameSettingsString = JsonUtility.ToJson(gameSettings);
                System.IO.File.WriteAllText(Application.persistentDataPath + "/GameSettings.json", gameSettingsString);
                currentGameSettings = gameSettings;
            }
            else
            {
                 foreach (var file in directoryInfo.GetFiles("*.json"))
                {   
                    Debug.Log("Game settings found.");
                    StreamReader reader = file.OpenText();
                    gameSettings = JsonUtility.FromJson<GameSettings>(reader.ReadToEnd());
                    areWhiteStrobesEnabled = !gameSettings.areWhiteStrobesEnabled;
                    if(!areWhiteStrobesEnabled)
                    {
                        strobeYesNo[0].SetActive(false);
                        strobeYesNo[1].SetActive(true);
                    }
                    else if(areWhiteStrobesEnabled)
                    {
                        strobeYesNo[0].SetActive(true);
                        strobeYesNo[1].SetActive(false);
                    }
                    lookSensitivity = gameSettings.lookSensitivity;

                    currentGameSettings = gameSettings;
                }
            }
        }
    }

    public void ToggleWhiteStrobing()
    {
        Debug.Log("strobing toggled");
        areWhiteStrobesEnabled = !areWhiteStrobesEnabled;
        if(areWhiteStrobesEnabled)
        {
            strobeYesNo[0].SetActive(true);
            strobeYesNo[1].SetActive(false);
            Debug.Log("strobing toggled 0");
        }
        else if(!areWhiteStrobesEnabled)
        {
            strobeYesNo[0].SetActive(false);
            strobeYesNo[1].SetActive(true);
            Debug.Log("strobing toggled 1");
        }
    }

    // When back button is pressed settings get saved
    public void SaveSettings()
    {
        GameSettings gameSettings = new GameSettings();
        gameSettings.areWhiteStrobesEnabled = !areWhiteStrobesEnabled;
        gameSettings.lookSensitivity = lookSensitivity;
        string gameSettingsString = JsonUtility.ToJson(gameSettings);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/GameSettings.json", gameSettingsString);
        currentGameSettings = gameSettings;
    }

    public void PlayMenuClickSound()
    {
        audioSource.PlayOneShot(menuClickSound);
    }
}
