using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunglasses : Item
{
    void UseObject()
    {
        if(playerStatusComponent.statusList[0] == myStatus && playerStatusComponent.RemoveStatus(myStatus))
        {
            messageManager.DisplayPickUpMessage(itemDescription);
            Destroy(this.gameObject);
        }
    }
}