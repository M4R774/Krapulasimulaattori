using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Usable
{

    public override void UseObject() {
        base.UseObject();
        Destroy(this.gameObject, 1);
    }
}