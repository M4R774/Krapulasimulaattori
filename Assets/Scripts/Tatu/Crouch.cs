using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FPSControllerLPFP;

public class Crouch : MonoBehaviour
{
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] GameObject fpsController;
    float capsuleColliderHeight;
    // Used when heart rate exceed certain value;
    public bool forceCrouch = false;

    void Start()
    {
        capsuleColliderHeight = capsuleCollider.height;
    }

    void Update()
    {
        if((Input.GetButtonDown("Crouch") || forceCrouch) && !fpsController.GetComponent<FpsControllerLPFP>().isCrouching)
        {
            GoToCrouch();
        }
        else if(Input.GetButtonDown("Crouch") && fpsController.GetComponent<FpsControllerLPFP>().isCrouching && !forceCrouch)
        {
            StandUp();
        }
    }

    public void StandUp()
    {
        // fpsController.GetComponent<FpsControllerLPFP>().isCrouching = false;
        capsuleCollider.height = capsuleColliderHeight;
        this.gameObject.SendMessage("Crouch", SendMessageOptions.DontRequireReceiver);
        forceCrouch = false;
    }

    public void GoToCrouch()
    {
        // fpsController.GetComponent<FpsControllerLPFP>().isCrouching = true;
        capsuleCollider.height = 0;
        this.gameObject.SendMessage("Crouch", SendMessageOptions.DontRequireReceiver);
    }
}

