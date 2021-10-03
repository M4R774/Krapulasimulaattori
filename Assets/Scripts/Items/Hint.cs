using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum hints
{
    lightHint,
    shakingHint,
    invertedHint,
    heartRateHint
}
public class Hint : MonoBehaviour
{
    [SerializeField] hints myHint;
    [SerializeField] HintsManager hintsManager;
    void UseObject()
    {
        hintsManager.ShowHint(myHint);
    }
}
