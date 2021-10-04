using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] double baselineHeartRate = 90; // BPM
    [SerializeField] double heartRateBPM; // Heart rate as BPM
    AudioSource heartAudioSource;
    GameObject heart;
    [Tooltip("If this threshold is exceeded to player crouches and moves slower."),SerializeField] double heartRateThreshold;
    Animator heartAnimator;
    [SerializeField] Crouch crouchScriprt;
    [SerializeField] MessageManager messageManager;

    void Start()
    {
        StartCoroutine("HeartRateCheckRoutine");
        GameObject[] heartAnimatorGameObjects = GameObject.FindGameObjectsWithTag("HeartUI");
        if (heartAnimatorGameObjects.Length > 0)
        {
            heart = heartAnimatorGameObjects[0];
            heartAudioSource = heart.GetComponent<AudioSource>();
            heartAnimator = heart.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Error: Could not find the HeartUI gameobject. Is " +
                "the gameobject with the heart animator tagged with 'HeartUI'? ");
        }
        ResetHeartRateToBaseline();
        StartCoroutine("PlayHeartSound");
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Tarkista oletko s�ngyss�
        double newHeartRate = heartRateBPM + Time.deltaTime; // 1s in irl -> +1 BPM to heart rate
        SetHeartRate(newHeartRate);
        // TODO: N�kym�n reunoilla n�kyv� punainen syke
    }

    private void FixedUpdate()
    {
        
    }

    public IEnumerator HeartRateCheckRoutine()
    {
        while(true)
        {
            if (heartRateBPM > heartRateThreshold)
            {
                crouchScriprt.forceCrouch = true;
                messageManager.DisplayDialogue("My heart is about to burst!*I need to go back to bed.");
                // TODO add audiosource and audio clip for dialog
            }
            else
            {
                crouchScriprt.forceCrouch = false;
            }
            yield return new WaitForSeconds(1);
        }
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
        return bpm / 72;
    }

    double ConvertAnimationSpeedToBPM(double animationSpeed)
    {
        return animationSpeed * 72;
    }

    IEnumerator PlayHeartSound()
    {
        // TODO: Adjust volume
        while (true)
        {
            heartAudioSource.volume = (float)((heartRateBPM - 100) / 100);
            heartAudioSource.Play();
            yield return new WaitForSeconds((float)(60f / heartRateBPM));
        }
    }
}
