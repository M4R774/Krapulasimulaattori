using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSControllerLPFP;

public class GrillFood : Usable
{
    [SerializeField] GameObject foodMesh;
    public override void OnUseItem()
    {
        if(foodMesh.activeSelf)
        {
            messageManager.DisplayDialogueAndPlayAudio(itemDescription, audioClips);
            GameEvents.current.OnGrillFoodEaten();
            foodMesh.SetActive(false);
        }
    }
}
