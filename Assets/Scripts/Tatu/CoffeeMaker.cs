using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMaker : Item
{
    [TextArea(5,5)]
    [SerializeField] string makeCoffeeDialogue;
    void Start()
    {
        GameEvents.current.onCoffeePackPickedUp += OnCoffeePackPickedUp;
    }
    private void OnCoffeePackPickedUp()
    {
        usable = true;
    }
    void UseObject()
    {
        if(usable)
        {
            messageManager.DisplayDialogue(makeCoffeeDialogue);
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