using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Usable
{
    public GameObject whiteoutSquare;
    public Light[] lights;

    public override void OnUseItem() {
        // Toggle whiteout
        if (whiteoutSquare.activeSelf) {
            whiteoutSquare.GetComponent<Whiteout>().enabled = false;
            whiteoutSquare.SetActive(false);
            itemDescription = "Lights out.";
            if (lights.Length != 0) {
                foreach (Light light in lights)
                {
                    light.gameObject.SetActive(false);
                }
            }
        } else {
            whiteoutSquare.SetActive(true);
            whiteoutSquare.GetComponent<Whiteout>().enabled = true;
            itemDescription = "Ouch the light hurts my eyes!";
            if (lights.Length != 0) {
                foreach (Light light in lights)
                {
                    light.gameObject.SetActive(true);
                }
            }
        }
        messageManager.DisplayDialogueAndPlayAudio(itemDescription, audioClips);
    }
}
