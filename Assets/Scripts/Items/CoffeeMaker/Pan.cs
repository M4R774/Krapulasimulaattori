using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : Usable
{
    [SerializeField] CameraShake cameraShake;

    public override void OnUseItem()
    {
        cameraShake.enabled = false;
    }
}
