using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMaker : Usable
{   
    [SerializeField] GameObject coffeePan;
    [TextArea(5,5)]
    [SerializeField] string makeCoffeeDialogue;
    [SerializeField] string makingCoffeeDialogue;
    [SerializeField] string coffeeReadyDialogue;
    private bool makingCoffee = false;
    private bool coffeeReady = false;
    private bool coffeeTimerStarted = false;
    [SerializeField] private float coffeeTimer = 10f;

    // Reaction audio
    [SerializeField] AudioClip reactionClip;
    AudioSource _innerAudioSource;

    // Countdown
    [SerializeField] CountdownTexture countdown;

    void Start()
    {
        _innerAudioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
        GameEvents.current.onCoffeePackPickedUp += OnCoffeePackPickedUp;
        coffeePan.tag = "Untagged";
        coffeePan.layer = 0;
    }

    void Update() {
        if (coffeeTimerStarted)
        {
            if (coffeeTimer > 0)
            {
                coffeeTimer -= Time.deltaTime;
            }
            else
            {
                coffeeReady = true;
                coffeePan.tag = "Interact";
                coffeePan.layer = 6;
                // stop this code getting called
                coffeeTimerStarted = false;
            }
        }
    }
    private void OnCoffeePackPickedUp()
    {
        usable = true;
    }
    public override void UseObject()
    {
        if(usable)
        {
            if (!makingCoffee && !coffeeReady)
            {
                messageManager.DisplayDialogueAndPlayAudio(makeCoffeeDialogue, audioClips);
                makingCoffee = true;
                countdown.StartCountDown(coffeeTimer);
                coffeeTimerStarted = true;
            }
            else if(makingCoffee && !coffeeReady )
            {
                messageManager.DisplayDialogueAndPlayAudio(makingCoffeeDialogue, audioClips);
            }
            else
            {
                messageManager.DisplayDialogueAndPlayAudio(coffeeReadyDialogue, audioClips);
            }
            //if(playerStatusComponent.statusList[0] == myStatus && playerStatusComponent.RemoveStatus(myStatus))
            //{
                
            //}
        }
        else
        {
            //_innerAudioSource.PlayOneShot(reactionClip);
            messageManager.DisplayDialogueAndPlayAudio(itemDescription, audioClips);
        }
    }

    void OnDestroy()
    {
        GameEvents.current.onPainKillerConsumed -= OnCoffeePackPickedUp;
    }
}