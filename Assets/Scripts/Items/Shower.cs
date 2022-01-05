using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : Usable
{
    [SerializeField] private Animator animator;
    private bool isShowerOn = false;
    [SerializeField] private AudioSource audioSource;
    public override void OnUseItem() {

        // Toggle shower's water
        if(!isShowerOn)
        {
            animator.SetTrigger("ShowerOn");
            isShowerOn = true;
            audioSource.Play();
        }
        else if(isShowerOn)
        {
            animator.SetTrigger("ShowerOff");
            isShowerOn = false;
            audioSource.Stop();
        }
    }
}
