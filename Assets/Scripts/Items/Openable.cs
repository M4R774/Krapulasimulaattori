using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Openable : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    public bool isOpenable;
    [SerializeField] protected MessageManager messageManager;
    [SerializeField] protected List<AudioClip> audioClips;
    [TextArea(5,5)]
    // This is displayed in the dialogue when the door is locked
    public string lockedDescription;
    
    void Awake()
    {
        if(!isOpenable)
        {
            rb.isKinematic = true;
        }
    }
    void Start()
    {
        if(messageManager == null)
            messageManager = FindObjectOfType<MessageManager>();
    }
    public virtual void Open()
    {
        if(!isOpenable)
        {
            messageManager.DisplayDialogueAndPlayAudio(lockedDescription, audioClips);
        }
    }

    public void ActivateOpenable()
    {
        isOpenable = true;
        rb.isKinematic = false;
    }

}
