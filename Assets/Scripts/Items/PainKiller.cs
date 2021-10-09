using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSControllerLPFP;

public class PainKiller : Consumable
{
    [SerializeField] FpsControllerLPFP fpsController;

    public override void OnUseItem()
    {
        messageManager.DisplayDialogueAndPlayAudio(itemDescription, audioClips);
        GameEvents.current.PainKillerConsumed();
        fpsController.toggleInversion = false;
    }
}
