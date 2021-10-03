using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSControllerLPFP;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] double baselineHeartRate = 90; // BPM
    [SerializeField] double heartRateBPM; // Heart rate as BPM
    [Tooltip("If this threshold is exceeded to player crouches and moves slower."),SerializeField] double heartRateThreshold;
    Animator heartAnimator;
    [SerializeField] FpsControllerLPFP fpsController;

    void Start()
    {
        StartCoroutine(HeartRateCheckRoutine());
        GameObject[] heartAnimatorGameObjects = GameObject.FindGameObjectsWithTag("HeartUI");
        if (heartAnimatorGameObjects.Length > 0)
        {
            heartAnimator = heartAnimatorGameObjects[0].GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Error: Could not find the HeartUI gameobject. Is " +
                "the gameobject with the heart animator tagged with 'HeartUI'? ");
        }
        ResetHeartRateToBaseline();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Tarkista oletko sängyssä
        double newHeartRate = heartRateBPM + Time.deltaTime; // 1s in irl -> +1 BPM to heart rate
        SetHeartRate(newHeartRate);
        // TODO: Näkymän reunoilla näkyvä punainen syke
    }

    private void FixedUpdate()
    {
        
    }

    public IEnumerator HeartRateCheckRoutine()
    {
        if (heartRateBPM > heartRateThreshold)
        {
            fpsController.forceCrouch = true;
        }
        else
        {
            fpsController.forceCrouch = false;
        }
        yield return null;
    }

    public void ResetHeartRateToBaseline()
    {
        SetHeartRate(baselineHeartRate);
    }

    void SetHeartRate(double bpm)
    {
        heartRateBPM = bpm;
        double animationSpeed = ConvertBPMToAnimationSpeed(bpm);
        heartAnimator.SetFloat("heartRateAnimationSpeed", (float)animationSpeed);
    }

    double ConvertBPMToAnimationSpeed(double bpm)
    {
        return bpm / 120;
    }

    double ConvertAnimationSpeedToBPM(double animationSpeed)
    {
        return animationSpeed * 120;
    }
}
