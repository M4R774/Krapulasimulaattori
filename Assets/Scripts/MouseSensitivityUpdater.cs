using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSensitivityUpdater : MonoBehaviour
{
    float sensitivity;
    [SerializeField] private TextMeshProUGUI sensitivityText;
    [SerializeField] private Slider slider;
    [SerializeField] private MainMenuManager mainMenuManager;

    // void Start()
    // {
    //     slider.value = mainMenuManager.currentGameSettings.lookSensitivity;
    //     sensitivityText.text = slider.value.ToString();
    // }

    void OnEnable()
    {
        Debug.Log("Mouse sensitivity updater was set active");
        slider.value = mainMenuManager.currentGameSettings.lookSensitivity;
        sensitivityText.text = slider.value.ToString();
    }

    public void SetSensitivity()
    {
        sensitivity = (Mathf.Round(slider.value * 10.0f) * 0.1f);
        sensitivityText.text = sensitivity.ToString();
        mainMenuManager.lookSensitivity = sensitivity;
    }
}
