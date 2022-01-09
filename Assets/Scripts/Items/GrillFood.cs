using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSControllerLPFP;

public class GrillFood : Consumable
{
    [SerializeField] GameObject foodMesh;
    public override void UseObject()
    {
        if(foodMesh != null && foodMesh.activeSelf)
        {
            messageManager.DisplayDialogueAndPlayAudio(itemDescription, audioClips);
            GameEvents.current.OnGrillFoodEaten();
            //if(itemPickUpCoroutine == null)
                //itemPickUpCoroutine =  StartCoroutine(ItemPickUp(Camera.main.transform.position - new Vector3(0,0.05f,0), lerpDuration));
        }
        base.UseObject();
    }
    private IEnumerator ItemPickUp(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = foodMesh.transform.position;
        Vector3 startScale = foodMesh.transform.localScale;
        Vector3 targetScale = foodMesh.transform.localScale * 0.15f;

        while (time < duration)
        {
            foodMesh.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            foodMesh.transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;  
        }
        foodMesh.transform.position = targetPosition;
        foodMesh.transform.localScale = targetScale;
        Destroy(foodMesh);
        yield return null;
    }
}
