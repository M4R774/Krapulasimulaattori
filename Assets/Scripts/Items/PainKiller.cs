using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSControllerLPFP;

public class PainKiller : Consumable
{
    [SerializeField] FpsControllerLPFP fpsController;

    // Reaction audio
    [SerializeField] AudioClip reactionClip;
    private AudioSource _innerAudioSource;

    void Start()
    {
        _innerAudioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
    }

    public override void OnUseItem()
    {
        _innerAudioSource.PlayOneShot(reactionClip);
        messageManager.DisplayDialogue(itemDescription);
        GameEvents.current.PainKillerConsumed();
        fpsController.toggleInversion = false;
    }
}
