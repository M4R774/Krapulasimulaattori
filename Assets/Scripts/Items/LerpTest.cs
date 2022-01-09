using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    private float lerpDuration = 0.4f;
    private Coroutine pickUpCoroutine;
    public Transform targetTransform;

    /*void Start()
    {
        StartCoroutine(ItemPickUp(targetTransform.position, lerpDuration));
    }*/

    public void ActivateItemPickUp(Vector3 pos)
    {
        StartCoroutine(ItemPickUp(pos, lerpDuration));
    }
    private IEnumerator ItemPickUp(Vector3 targetPosition, float duration)
    {
        //yield return new WaitForSeconds(2f);
        float time = 0;
        Vector3 startPosition = transform.position;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = transform.localScale * 0.15f;
        this.GetComponent<Collider>().enabled = false;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            //transform.localScale = startScale * Vector3.Distance(transform.position, targetPosition);
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;  
        }
        transform.position = targetPosition;
        transform.localScale = targetScale;
        Destroy(this.gameObject);
        yield return null;
    }

    void MoveTowards()
    {
        float step =  lerpDuration * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, step);
    }
}