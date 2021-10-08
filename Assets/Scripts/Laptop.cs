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

    void Start()
    {
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
            if(audioSource != null && audioSource.clip != null)
                audioSource.Play();
        }
    }

    public virtual void UseObject() {
        messageManager.DisplayDialogueAndPlayAudio(itemDescription, audioClips);
    }
}
