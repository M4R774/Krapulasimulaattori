using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainKiller : Item
{
    void UseObject()
    {
        if(playerStatusComponent.statusList[0] == myStatus && playerStatusComponent.RemoveStatus(myStatus))
        {
            messageManager.DisplayDialogue(itemDescription);
            GameEvents.current.PainKillerConsumed();
            Destroy(this.gameObject);
        }
    }
}
