using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Usable
{
    public GameObject whiteoutSquare;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnUseItem() {
        // Toggle whiteout
        whiteoutSquare.SetActive(!whiteoutSquare.activeSelf);
    }
}
