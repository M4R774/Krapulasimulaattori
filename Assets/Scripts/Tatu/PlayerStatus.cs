using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//
// Tracks and changes player's status
// Statuses are things the player needs to get rid of
//

public enum Status
{
    needSunglasses,
    needPainkillers,
    needCoffee
}
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] public List<Status> statusList;
    [SerializeField] DragRigidbodyUse dragRigidbodyUse;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI statusText;

    void Start()
    {
        InitStatusList();
    }

    void Update()
    {
        if (statusList.Count != 0)
            statusText.text = statusList[0].ToString();
        else
            statusText.text = "";
    }

    public void InitStatusList() // public so can be called from outside if needed
    {
        statusList.Add(Status.needSunglasses);
        statusList.Add(Status.needPainkillers);
        statusList.Add(Status.needCoffee);
    }

    public bool RemoveStatus(Status st)
    {
        if (HasStatus(st)) {
            statusList.Remove(st);
            dragRigidbodyUse.ObjectUsed();
            return true;
        } else {
            return false;
        }
    }

    public bool HasStatus(Status st)
    {
        return statusList.Contains(st);
    }
}
