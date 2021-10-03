using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crouch : MonoBehaviour
{
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] GameObject fpsController;
    float capsuleColliderHeight;
    bool isCrouching = false;

    void Start()
    {
        capsuleColliderHeight = capsuleCollider.height;
    }

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
            capsuleCollider.height = capsuleColliderHeight;
            this.gameObject.SendMessage("Crouch",SendMessageOptions.DontRequireReceiver);
        }
    }
}

