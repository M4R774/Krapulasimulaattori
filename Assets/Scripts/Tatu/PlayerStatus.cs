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
// classic is the game jam version
// enhanced uses new ending and point counting
public enum GameMode
{
    classic,
    enhanced
}
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] public List<Status> statusList;
    [SerializeField] DragRigidbodyUse dragRigidbodyUse;
    [SerializeField] MessageManager messageManager;
    public bool canPlayerMove = false;
    [SerializeField] HideCursor hideCursor;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI statusText;

    [Header("Tasks")]
    [SerializeField] int timeUntilWork = 120; // in seconds
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
    [SerializeField] GameMode gameMode;
    [SerializeField] GameObject endingScreen;
    [SerializeField] GameObject heartGO;
    [SerializeField] GameObject pointerCanvas;
    [SerializeField] Clock clock;
    float timer = 0.0f;

    int score = 75;
    [TextArea]
    [SerializeField] string[] endingConditions;
    [TextArea]
    [SerializeField] string[] endingResults;
    List<string> conditions = new List<string>();

    [SerializeField] GraphicRaycaster inGameUIGraphicRaycaster;

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
        timer = 0.0f;
        _innerAudioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
        InitStatusList();
        InitTasks();
        if (GameEvents.current != null)
        {
            GameEvents.current.onTriggerVolumeExit += OnTriggerVolumeExit;
            GameEvents.current.onShowerEnter += OnShowerEnter;
            GameEvents.current.onGrillFoodEaten += OnGrillFoodEaten;
        }

        if(gameMode == null)
        {
            gameMode = GameMode.classic;
        }
    }

    void Update()
    {
        /*if (statusList.Count != 0)
            statusText.text = statusList[0].ToString();
        else
            statusText.text = "";*/
        
        CheckTasks();
        // timer should only run after the player can move
        if(canPlayerMove)
            timer += Time.deltaTime;
        //Debug.Log(Mathf.Round(timer));
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

    // Front door calls this to check if player can exit
    public bool CanOpenDoor()
    {
        if(gameMode == GameMode.classic)
        {
            if(!isShaking && !isInverted && !areLightsOn && !highHeartRate)
                return true;
            else
            {
                return false;
            }
        }
        else if(gameMode == GameMode.enhanced)
        {
            CheckConditions();
            return true;
        }
        else
            return false;
    }

    void CheckConditions()
    {
        if(isShaking)
        {
            score -= 25;
            conditions.Add(endingConditions[0]);
        }
        if(isInverted)
        {
            score -= 25;
            conditions.Add(endingConditions[1]);
        }
        if(areLightsOn)
        {
            score -= 25;
            conditions.Add(endingConditions[2]);
        }
        if(highHeartRate)
        {
            score -= 25;
            conditions.Add(endingConditions[3]);
        }
        Debug.Log("player score " + score);
        foreach (var item in conditions)
        {
            Debug.Log(item);
        }
    }

    public void EndGame()
    {
        heartGO.SetActive(false);
        pointerCanvas.SetActive(false);
        dragRigidbodyUse.enabled = false;
        StartCoroutine("EndingScreen");
        GameObject.Find("GameManager").GetComponent<PauseGame>().DisablePause();
    }
    IEnumerator EndingScreen()
    {
        TextMeshProUGUI victoryText = endingScreen.GetComponentInChildren<TextMeshProUGUI>();
        float timerTime = Mathf.Round(timer);
        string endingText;
        float origFontSize = victoryText.fontSize;
        int timeBonus = 0;
        string removedPoints = "\n-25";
        if(timerTime < timeUntilWork)
        {
            endingText = "You managed to leave for work in time. \n" +
            "It only took you " + timerTime +" seconds!";
            // This needs to be balanced somehow
            // Now games takes about 30sec to go thru if coffee is boiled first
            // and 35sec if coffee is boiled last
            if(timerTime < 60)
            {   
                timeBonus = 10;
                score += timeBonus;
                if(timerTime <= 30)
                {
                    Debug.Log("Timer was " + timerTime);
                    timeBonus += 15;
                    score += 15;
                }
                //timeBonus = (int) ExtensionMethods.Remap(timer, 0, 100, 100, 0);
                //score += timeBonus;
            }
        }
        else
        {
            endingText = "You were late for work. \n" +
            "It took you " + timerTime +" seconds to leave.";
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
            Color origColor = endingScreen.GetComponent<Image>().color;
            endingScreen.GetComponent<Image>().color = new Color(origColor.r, origColor.g, origColor.b, i);
            victoryText.color = new Color(1, 1, 1, i);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(3f);
        if(conditions.Count != 0)
        {
            victoryText.fontSize = 100;
            victoryText.text = "BUT!";
            yield return new WaitForSeconds(1f);
            victoryText.fontSize = origFontSize;
            foreach (var condition in conditions)
            {
                victoryText.text = condition;
                yield return new WaitForSeconds(1f);
                victoryText.text += removedPoints;
                yield return new WaitForSeconds(0.75f);
            }
        }
        if(timeBonus != 0)
        {
            victoryText.text = "Time bonus" ;
            yield return new WaitForSeconds(1f);
            victoryText.text += "\n+" + timeBonus.ToString();
            yield return new WaitForSeconds(0.75f);
        }
        yield return new WaitForSeconds(0.25f);
        victoryText.text = "Your final score is " + score.ToString() + ".";
        yield return new WaitForSeconds(2f);
        // show cursor so player can retry or quit
        hideCursor.enabled = true;
        inGameUIGraphicRaycaster.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        endingScreen.transform.Find("Buttons").gameObject.SetActive(true);
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
