using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reflects the heart status
// Script that manages the heartbeat animation and audio
public class HeartManager : MonoBehaviour
{

    [SerializeField] PlayerStatsManager playerStatsManager;

    private int heartRate;

    void Start()
    {
        heartRate = playerStatsManager.getHearRate();
    }

    void Update()
    {
        heartRate = playerStatsManager.getHearRate();
    }
}
