using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FPSControllerLPFP;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//
// Acts as something like a task manager, that tracks player's progress
//

public enum Status
{
    needSunglasses,
    needPainkillers,
    needCoffee,
    needShower,
    needEarplugs,
}
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] public List<Status> statusList;
    [SerializeField] DragRigidbodyUse dragRigidbodyUse;
    [SerializeField] MessageManager messageManager;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI statusText;

    [Header("Tasks")]
    [SerializeField] bool areLightsOn;
    [SerializeField] GameObject lightImage;
    public bool isShaking;
    [SerializeField] CameraShake cameraShake;
    public bool isInverted;
    [SerializeField] FpsControllerLPFP fpsController;
    [SerializeField] bool highHeartRate;
    [SerializeField] PlayerStats playerStats;
    public bool hasShowered = false;
    [TextArea]
    [SerializeField] string textToDisplayWhenShowered;
    [SerializeField] List<AudioClip> audioToPlayWhenShowered;
    [SerializeField] bool hasEaten = false;

    // The following texts are what are displayed if the player tries to exit
    // and they haven't done all the tasks
    [Header("Door opening reactions")]
    [TextArea]
    [SerializeField] string lightsAreOnText;
    [TextArea]
    [SerializeField] string stillShakingText;
    [TextArea]
    [SerializeField] string controlsInvertedText;
    [TextArea]
    [SerializeField] string highHeartRateText;
    [TextArea]
    [SerializeField] string hasShoweredText;
    [TextArea]
    [SerializeField] string hasEatenText;

    [Header("Ending")]
    [SerializeField] GameObject endingScreen;
    [SerializeField] GameObject heartGO;
    [SerializeField] GameObject pointerCanvas;
    [SerializeField] Clock clock;
    float timer = 0.0f;

    [Header("Trigger Volume Logic")]
    [SerializeField] bool bedroomVolumeTrigger = false;

    [Header("Reaction audio")]
    [SerializeField] AudioClip cantleave;
    private AudioClip shaking;
    private AudioClip cantmove;
    private AudioClip lights;
    private AudioClip heart;
    private AudioSource _innerAudioSource;

    void Start()
    {
        _innerAudioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
        InitStatusList();
        InitTasks();
        if (GameEvents.current != null)
        {
            GameEvents.current.onTriggerVolumeExit += OnTriggerVolumeExit;
            GameEvents.current.onShowerEnter += OnShowerEnter;
            GameEvents.current.onGrillFoodEaten += OnGrillFoodEaten;
        }
    }

    void Update()
    {
        /*if (statusList.Count != 0)
            statusText.text = statusList[0].ToString();
        else
            statusText.text = "";*/
        
        CheckTasks();
        timer += Time.deltaTime;
    }

    void CheckTasks()
    {
        areLightsOn = lightImage.activeSelf;
        highHeartRate = playerStats.IsHeartRateTooHigh();
        isShaking = cameraShake.enabled;
        isInverted = fpsController.toggleInversion;

        if(!bedroomVolumeTrigger)
        {
            // randomly choosing effects could go here
            // or earlier if the player should know them right away
            cameraShake.enabled = false;
            fpsController.toggleInversion = false;
        }
    }

    public void InitStatusList() // public so can be called from outside if needed
    {
        statusList.Add(Status.needSunglasses);
        statusList.Add(Status.needPainkillers);
        statusList.Add(Status.needCoffee);
    }

    //
    // Negative effects are initialised here!
    // Uncomment lines isShaking and isInverted to enable those negative effects in the beginning
    //
    public void InitTasks()
    {
        areLightsOn = true; // this will always be true
        //isShaking = cameraShake.enabled;
        //isInverted = fpsController.toggleInversion;
    }

    public bool RemoveStatus(Status st)
    {
        if (HasStatus(st)) {
            statusList.Remove(st);
            dragRigidbodyUse.ObjectUsed();
            return true;
        } else {
            return false;
        }
    }

    public bool HasStatus(Status st)
    {
        return statusList.Contains(st);
    }

    public bool CanOpenDoor()
    {
        if(!isShaking && !isInverted && !areLightsOn && !highHeartRate)
            return true;
        else
        {
            return false;
        }
    }

    public void EndGame()
    {
        heartGO.SetActive(false);
        pointerCanvas.SetActive(false);
        dragRigidbodyUse.enabled = false;
        StartCoroutine("EndingScreen");
    }
    IEnumerator EndingScreen()
    {
        TextMeshProUGUI victoryText = endingScreen.GetComponentInChildren<TextMeshProUGUI>();
        float timerTime = Mathf.Round(timer);
        string endingText;
        if(clock.hour < 7)
        {
            endingText = "You managed to leave for work in time. \n" +
            "It only took you " + timerTime % 60 +" seconds!";
        }
        else
        {
            endingText = "You were late for work. \n" +
            "It took you " + timerTime % 60 +" seconds to leave.";
        }
        /*string endingText = "You managed to leave for work! \n" +
            "You left home at: " + clock.hour + ":" + clock.minutes + ":" + clock.seconds + "\n";
        if (clock.hour > 7)
        {
            endingText += "\nYou were " + (((clock.hour-8)*60) + clock.minutes) + " minutes late.\n";
        }
        else
        {
            endingText += "\nYou were on time with " + (((clock.hour - 8) * -60) - clock.minutes) + " minutes to spare!\n";
        }*/
        endingScreen.SetActive(true);
        victoryText.text = endingText;
        for (float i = 0; i < 1; i += 0.01f)
        {
            endingScreen.GetComponent<Image>().color = new Color(0, 0, 0, i);
            victoryText.color = new Color(1, 1, 1, i);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("MainMenu");
    }

    public string TaskList()
    {
        string toDolist = "I can't leave yet.";
        //_innerAudioSource.PlayOneShot(cantleave);
        if (areLightsOn)
        {
            // TODO: Figure out how to queue voicelines....
            toDolist = toDolist + "*" + lightsAreOnText;
        }
        if(isShaking)
        {
            toDolist = toDolist + "*" + stillShakingText;
        }
        if(isInverted)
        {
            toDolist = toDolist + "*" + controlsInvertedText;
        }
        if(highHeartRate)
        {
            toDolist = toDolist + "*" + highHeartRateText;
        }

        return toDolist;
    }

    void OnTriggerVolumeExit(TriggerVolumeID sendersID)
    {
        if(sendersID == TriggerVolumeID.bedroom)
        {
            if(!bedroomVolumeTrigger)
            {
                cameraShake.enabled = true;
                fpsController.toggleInversion = true;
            }
            bedroomVolumeTrigger = true;
        }
    }

    void OnShowerEnter(string sendersID)
    {
        if(sendersID == "Water")
        {   
            hasShowered = true;
            messageManager.DisplayDialogueAndPlayAudio(textToDisplayWhenShowered, audioToPlayWhenShowered);
        }
    }

    void OnGrillFoodEaten()
    {
        hasEaten = true;
    }

    void OnDestroy()
    {
        if (GameEvents.current != null)
        {
            GameEvents.current.onTriggerVolumeExit -= OnTriggerVolumeExit;
            GameEvents.current.onShowerEnter -= OnShowerEnter;
            GameEvents.current.onGrillFoodEaten -= OnGrillFoodEaten;
        }
    }

}
