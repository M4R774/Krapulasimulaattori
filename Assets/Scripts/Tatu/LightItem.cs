using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightItem : Item
{
    [SerializeField] Light light;
    [SerializeField] float onIntensity;
    [SerializeField] float offIntensity;
    
    void Start()
    {
        if(usable)
        {
            light.intensity = onIntensity;
        }
        else
            light.intensity = offIntensity;
    }
    void UseObject()
    {
        if(usable)
        {
            light.intensity = onIntensity;
        }
        else
            light.intensity = offIntensity;

        usable = !usable;
    }
}
