using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : Door
{
    [SerializeField] PlayerStatus playerStatus;
    
    public override void Open()
    {
        if(playerStatus.CanOpenDoor())
        {
            playerStatus.EndGame();
        }
        else
        {
            messageManager.DisplayDialogueAndPlayAudio(playerStatus.TaskList(), audioClips);
        }
    }
}