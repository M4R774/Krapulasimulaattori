using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Class for items that are either pick-upped or consumed
//
public abstract class Item : MonoBehaviour
{
    public bool usable;
    public Status myStatus;
    public PlayerStatus playerStatusComponent;
    [SerializeField] protected MessageManager messageManager;

    [TextArea(5,5)]
    public string itemDescription;

    void Awake()
    {
        if(messageManager == null)
            messageManager = FindObjectOfType<MessageManager>();
    }

    void OnCollisionEnter(Collision col)
    {
        Vector3 collisionForce = col.impulse / Time.fixedDeltaTime;
        Debug.Log(collisionForce);
        GameEvents.current.OnCollisionSound(collisionForce, this.transform);
    }
}
