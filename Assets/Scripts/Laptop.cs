using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : Item
{
    [SerializeField] float breakForce = 4.0f;
    [SerializeField] Material breakMaterial;
    [SerializeField] bool broken = false;
    MeshRenderer meshRenderer;
    AudioSource audioSource;

    // Reaction audio
    [SerializeField] AudioClip reactionClip;
    AudioSource _innerAudioSource;

    void Start()
    {
        _innerAudioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!broken && collision.relativeVelocity.magnitude > breakForce)
        {
            Debug.Log(collision.relativeVelocity.magnitude);
            broken = true;
            meshRenderer.material = breakMaterial;
            audioSource.Play();
        }
    }

    public virtual void UseObject() {
        messageManager.DisplayDialogue(itemDescription);
        _innerAudioSource.PlayOneShot(reactionClip);
    }
}
