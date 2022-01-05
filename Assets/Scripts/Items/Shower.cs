using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : Usable
{
    [SerializeField] private Animator animator;
    private bool isShowerOn = false;

    public override void OnUseItem() {

        // Toggle shower's water
        if(!isShowerOn)
        {
            animator.SetTrigger("ShowerOn");
            isShowerOn = true;
        }
        else if(isShowerOn)
        {
            animator.SetTrigger("ShowerOff");
            isShowerOn = false;
        }
    }
}
