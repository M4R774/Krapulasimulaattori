using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : Item
{
    public virtual void UseObject()
    {
        OnUseItem();
    }

    public virtual void OnUseItem()
    {
        messageManager.DisplayDialogueAndPlayAudio(itemDescription, audioClips);
    }
}