using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Openable
{
    void Start()
    {
        GameEvents.current.onPainKillerConsumed += OnPainKillerConsumed;
    }
    private void OnPainKillerConsumed()
    {
        isOpenable = true;
        rb.isKinematic = false;
    }

    void OnDestroy()
    {
        GameEvents.current.onPainKillerConsumed -= OnPainKillerConsumed;
    }
}