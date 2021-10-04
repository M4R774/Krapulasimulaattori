using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : Usable
{
    [SerializeField] Material emptyBottleMaterial;
    [SerializeField] Material fullBottleMaterial;
    public GameObject whiteoutSquare;

    // Reaction audio
    [SerializeField] AudioClip reactionClip1;
    [SerializeField] AudioClip reactionClip2;
    private AudioSource _innerAudioSource;

    void Awake()
    {
        Renderer rend = GetComponentInChildren<Renderer>();
        if(usable)
            rend.material = fullBottleMaterial;
        else
        {
            rend.material = emptyBottleMaterial;
        }
    }

    void Start()
    {
        _innerAudioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
    }
    public override void UseObject()
    {
        // Separates the itemDescription according to if the item is usable or not
        string[] phrases = itemDescription.Split('*');
        if (Random.Range(0, 100) > 50) {
            messageManager.DisplayDialogue(phrases[0]);
            _innerAudioSource.PlayOneShot(reactionClip1);
        } else {
            messageManager.DisplayDialogue(phrases[1]);
            _innerAudioSource.PlayOneShot(reactionClip2);
            //Destroy(transform, 1);
        }
    }
}
