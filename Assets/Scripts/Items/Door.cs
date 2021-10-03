using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Openable
{
    void Start()
    {
        if (GameEvents.current != null)
        {
            GameEvents.current.onPainKillerConsumed += OnPainKillerConsumed;
        }
    }
    private void OnPainKillerConsumed()
    {
        isOpenable = true;
        rb.isKinematic = false;
    }

    void OnDestroy()
    {
        if (GameEvents.current != null)
        {
            GameEvents.current.onPainKillerConsumed -= OnPainKillerConsumed;
        }
    }
}