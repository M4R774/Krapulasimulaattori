using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crouch : MonoBehaviour
{
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] GameObject fpsController;
    bool isCrouching = false;

    void Update()
    {
        if(Input.GetButtonDown("Crouch") && !isCrouching)
        {
            isCrouching = true;
            capsuleCollider.height = 0;
            this.gameObject.SendMessage("Crouch",SendMessageOptions.DontRequireReceiver);
        }
        else if(Input.GetButtonDown("Crouch") && isCrouching)
        {
            isCrouching = false;
            capsuleCollider.height = 1;
            this.gameObject.SendMessage("Crouch",SendMessageOptions.DontRequireReceiver);
        }
    }
}

