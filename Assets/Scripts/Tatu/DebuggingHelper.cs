using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Helps debugging the game by deactivating the challenges in game
//
public class DebuggingHelper : MonoBehaviour
{
    #if UNITY_EDITOR

    [Help("Deactive this component OR the whole Game Object when you are done with debugging.", UnityEditor.MessageType.None)]

    public PlayerStats playerStats;
    public PlayerStatus playerStatus;
    public GameObject lightSensitivityEffect;

    void Start()
    {
        playerStatus.enabled = false;
        playerStats.enabled = false;
        StartCoroutine(DeactivateLightSensitivityEffect());
    }

    private IEnumerator DeactivateLightSensitivityEffect()
    {
        yield return new WaitForSeconds(8);
        lightSensitivityEffect.SetActive(false);
        yield return null;
    }
    #endif
}
