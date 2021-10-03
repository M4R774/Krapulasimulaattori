using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents current;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    public event Action onShake;
    public void Shake()
    {
        if(onShake != null)
        {
            onShake();
        }
    }
}
