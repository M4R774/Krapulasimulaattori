using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSControllerLPFP;

//
// Checks changes made into game settings json that can be chnaged in the main menu
// Settings found in MainMenuManager.cs
//

public class GameSettingsManager : MonoBehaviour
{
    [SerializeField] private Whiteout whiteout;
    [SerializeField] private Whiteout simpleWhiteout;
    private GameSettings gameSettings;

    [SerializeField] FpsControllerLPFP fpsController;

    void Start()
    { 
        UpdateSettings();
    }

    public void UpdateSettings()
    {
        if(Directory.Exists(Application.persistentDataPath))
        {
            string settingsPath = Application.persistentDataPath;
            DirectoryInfo directoryInfo = new DirectoryInfo(settingsPath);
            foreach (var file in directoryInfo.GetFiles("*.json"))
            {
                StreamReader reader = file.OpenText();
                gameSettings = JsonUtility.FromJson<GameSettings>(reader.ReadToEnd());
            }

            if(gameSettings.areWhiteStrobesEnabled)
            {
                whiteout.isStrobeEffectActive = true;
                whiteout.StartStrobingWhiteout();
            }
            else
            {
                whiteout.StopStrobingWhiteout();
            }
            fpsController.mouseSensitivity = gameSettings.lookSensitivity;

            Debug.Log(fpsController.mouseSensitivity);
        }
    }
}
