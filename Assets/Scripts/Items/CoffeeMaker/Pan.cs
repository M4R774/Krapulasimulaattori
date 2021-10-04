using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Usable
{
    [SerializeField] CameraShake cameraShake;

    // Reaction audio
    [SerializeField] AudioClip reactionClip;
    AudioSource _innerAudioSource;

    void Start()
    {
        _innerAudioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
    }

    public override void OnUseItem()
    {
        _innerAudioSource.PlayOneShot(reactionClip);
        cameraShake.enabled = false;
    }
}
