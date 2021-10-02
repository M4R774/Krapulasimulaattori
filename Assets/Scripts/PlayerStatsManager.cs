using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int heartRate;
    [SerializeField] int headache;
    [SerializeField] int sensitivyToLight;

    public const int NO_HEADACHE = 0;
    public const int LIGHT_HEADACHE = 1;
    public const int MEDIUM_HAEDACHE = 2;
    public const int HEAVY_HEADACHE = 3;

    public const int NO_SENSITIVITY_TO_LIGHT = 0;
    public const int LIGHT_SENSITIVITY_TO_LIGHT = 1;
    public const int MEDIUM_SENSITIVITY_TO_LIGHT = 2;
    public const int HEAVY_SENSITIVITY_TO_LIGHT = 3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeHeartRate(int change)
    {
        heartRate += change;
    }

    void changeHeadache(int change)
    {
        headache += change;
    }

    void changeSensitivityToLight(int change)
    {
        sensitivyToLight += change;
    }

    public int getHearRate()
    {
        return heartRate;
    }

}
