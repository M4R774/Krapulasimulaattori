using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : Usable
{
    [SerializeField] Material emptyBottleMaterial;
    [SerializeField] Material fullBottleMaterial;
    public GameObject whiteoutSquare;

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
    public override void UseObject()
    {
        // Separates the itemDescription according to if the item is usable or not
        string[] phrases = itemDescription.Split('*');
        if (usable) {
            messageManager.DisplayDialogue(phrases[0]);
        } else {
            messageManager.DisplayDialogue(phrases[1]);
            //Destroy(transform, 1);
        }
    }
}
