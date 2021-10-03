using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Usable
{
    public GameObject whiteoutSquare;

    public override void OnUseItem() {
        // Toggle whiteout
        whiteoutSquare.SetActive(!whiteoutSquare.activeSelf);
    }
}
