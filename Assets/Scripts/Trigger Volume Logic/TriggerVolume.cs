using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerVolumeID
{
    bedroom
}
public class TriggerVolume : MonoBehaviour
{
    [SerializeField] GameObject triggerVolumeReceiver;
    [SerializeField] TriggerVolumeID myTriggerVolumeID;

    void OnTriggerExit()
    {
        GameEvents.current.OnTriggerVolumeExit(myTriggerVolumeID);
    }
}
