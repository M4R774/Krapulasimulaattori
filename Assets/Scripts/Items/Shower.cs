using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Usable
{
    public GameObject whiteoutSquare;

    public override void OnUseItem() {
        // Toggle whiteout
        if (whiteoutSquare.activeSelf)
        {
            whiteoutSquare.GetComponent<Whiteout>().enabled = false;
            whiteoutSquare.SetActive(false);
            itemDescription = "Lights out.";
        }
    }
}
