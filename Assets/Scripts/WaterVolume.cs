using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Attach this to a trigger volume in a water mesh
//

public class WaterVolume : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            GameEvents.current.OnShowerEnter(this.tag);
    }
}