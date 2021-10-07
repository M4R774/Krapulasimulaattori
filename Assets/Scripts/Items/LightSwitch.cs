using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Usable
{
    public GameObject whiteoutSquare;

    public override void OnUseItem() {
        // Toggle whiteout
        if (whiteoutSquare.activeSelf) {
            whiteoutSquare.GetComponent<Whiteout>().enabled = false;
            whiteoutSquare.SetActive(false);
            itemDescription = "Lights out.";
        } else {
            whiteoutSquare.SetActive(true);
            whiteoutSquare.GetComponent<Whiteout>().enabled = true;
            itemDescription = "Ouch the light hurts my eyes!";
        }
        messageManager.DisplayDialogueAndPlayAudio(itemDescription, audioClips);
    }
}
