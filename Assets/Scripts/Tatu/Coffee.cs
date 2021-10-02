using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Item
{
    void UseObject()
    {
        messageManager.DisplayPickUpMessage(itemDescription);
        GameEvents.current.CoffeePackPickedUp();
        Destroy(this.gameObject);
    }
}
