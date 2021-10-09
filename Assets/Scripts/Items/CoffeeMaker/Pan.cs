using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Usable
{
    [SerializeField] CameraShake cameraShake;

    public override void OnUseItem()
    {
        messageManager.DisplayDialogueAndPlayAudio(itemDescription, audioClips);
        cameraShake.enabled = false;
    }
}
