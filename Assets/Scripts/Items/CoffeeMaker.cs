using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMaker : Usable
{
    [TextArea(5,5)]
    [SerializeField] string makeCoffeeDialogue;
    [SerializeField] string makingCoffeeDialogue;
    [SerializeField] string coffeeReadyDialogue;
    private bool makingCoffee = false;
    private bool coffeeReady = false;
    private bool coffeeTimerStarted = false;
    private float coffeeTimer = 0f;
    void Start()
    {
        GameEvents.current.onCoffeePackPickedUp += OnCoffeePackPickedUp;
    }

    void Update() {
        if (coffeeTimerStarted) {
            if (coffeeTimer > 0) {
                coffeeTimer -= Time.deltaTime;
            } else {
                coffeeReady = true;
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
            if (!makingCoffee && !coffeeReady) {
                messageManager.DisplayDialogue(makeCoffeeDialogue);
                makingCoffee = true;
                coffeeTimer = 10f;
                coffeeTimerStarted = true;
            } else if(makingCoffee && !coffeeReady ) {
                messageManager.DisplayDialogue(makingCoffeeDialogue);
            } else {
                messageManager.DisplayDialogue(coffeeReadyDialogue);
            }
            //if(playerStatusComponent.statusList[0] == myStatus && playerStatusComponent.RemoveStatus(myStatus))
            //{
                
            //}
        }
        else
        {
            messageManager.DisplayDialogue(itemDescription);
        }
    }

    void OnDestroy()
    {
        GameEvents.current.onPainKillerConsumed -= OnCoffeePackPickedUp;
    }
}