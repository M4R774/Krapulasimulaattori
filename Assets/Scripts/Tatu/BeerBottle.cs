using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottle : Item
{
    [SerializeField] Material emptyBottleMaterial;
    [SerializeField] Material fullBottleMaterial;

    void Awake()
    {
        Renderer rend = GetComponent<Renderer>();
        if(usable)
            rend.material = fullBottleMaterial;
        else
        {
            rend.material = emptyBottleMaterial;
        }
    }
    void UseObject()
    {
        // Separates the itemDescription according to if the item is usable or not
        string[] phrases = itemDescription.Split('*');
        if(usable)
            messageManager.DisplayDialogue(phrases[0]);
        else
        {
            messageManager.DisplayDialogue(phrases[1]);
        }
    }
}
