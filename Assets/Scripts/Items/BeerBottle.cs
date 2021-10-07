using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(SceneManager.GetActiveScene().name != "MainMenu")
            _innerAudioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
    }
    public override void UseObject()
    {
        // Separates the itemDescription according to if the item is usable or not
        // Send audio clip 0 or 1 depending on if item is usable or not
        string[] phrases = itemDescription.Split('*');
        if (Random.Range(0, 100) > 50) {
            List<AudioClip> usableBeerAudio = new List<AudioClip>();
            usableBeerAudio.Add(audioClips[0]);
            messageManager.DisplayDialogueAndPlayAudio(phrases[0], usableBeerAudio);
            //_innerAudioSource.PlayOneShot(reactionClip1);
        } else {
            List<AudioClip> unusableBeerAudio = new List<AudioClip>();
            unusableBeerAudio.Add(audioClips[1]);
            messageManager.DisplayDialogueAndPlayAudio(phrases[1], unusableBeerAudio);
            //_innerAudioSource.PlayOneShot(reactionClip2);
            //Destroy(transform, 1);
        }
    }
}
