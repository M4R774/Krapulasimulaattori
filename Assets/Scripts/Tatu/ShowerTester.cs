using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerTester : MonoBehaviour
{
    public Animator animator;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("ShowerOn");
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("ShowerOff");
        }
    }
}
