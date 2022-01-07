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
    [SerializeField] PlayerStats playerStats;
    Coroutine crouchCoroutine = null;
    [SerializeField] MessageManager messageManager;
    bool isCrouching = false;
    public bool canForceCrouch = true;

    void Start()
    {
        capsuleColliderHeight = capsuleCollider.height;
        if(messageManager == null)
            messageManager = FindObjectOfType<MessageManager>();
    }

    void Update()
    {
        if((Input.GetButtonDown("Crouch") || forceCrouch) && !isCrouching)
        {
            GoToCrouch();
        }
        else if(Input.GetButtonDown("Crouch") && isCrouching && !forceCrouch)
        {
            StandUp();
            if(playerStats.IsHeartRateTooHigh())
            {
                if(crouchCoroutine == null)
                    crouchCoroutine = StartCoroutine(CrouchRoutine());
            }
        }
        if(playerStats.IsHeartRateTooHigh() && canForceCrouch)
        {
            Debug.Log("Hearrate is too high");
            crouchCoroutine =  StartCoroutine(CrouchRoutine());
            canForceCrouch = false;
        }
    }

    public void StandUp()
    {
        isCrouching = false;
        capsuleCollider.height = capsuleColliderHeight;
        this.gameObject.SendMessage("Crouch", SendMessageOptions.DontRequireReceiver);
    }

    public void GoToCrouch()
    {
        isCrouching = true;
        capsuleCollider.height = 0;
        this.gameObject.SendMessage("Crouch", SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator CrouchRoutine()
    {
        Debug.Log("Started crouch routine");
        yield return new WaitForSeconds(.5f);
        GoToCrouch();
        messageManager.DisplayDialogueAndPlayAudio("My heart's about to burst!*I need to go back to bed.", playerStats.audioClips);
        crouchCoroutine = null;
        Debug.Log("ending crouch routine");
        yield return null;
    }

    public void ResetCrouchAfterSleeping()
    {
        isCrouching = false;
        capsuleCollider.height = capsuleColliderHeight;
    }
}
