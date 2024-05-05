using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : Usable
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    public override void OnUseItem()
    {
        if(!audioSource.isPlaying)
        {
            animator.SetTrigger("Flush");
            buttonAnimator.SetTrigger("ButtonDown");
            audioSource.PlayOneShot(audioClip);
            messageManager.DisplayDialogueAndPlayAudio(itemDescription, audioClips);
        }
    }
       
}
