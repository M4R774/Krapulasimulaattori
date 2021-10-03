using FPSControllerLPFP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Usable
{
    // Movement speed in units per second.
    public float speed = 0.01F;

    [SerializeField] GameObject player;
    FpsControllerLPFP fpsController;
    Rigidbody playerRigidbody;
    Collider playerCollider;
    Coroutine sleepCoroutine;

    [SerializeField] Transform sleepPosition;
    [SerializeField] Transform wakeUpPosition;
    public Transform startMarker;

    // Total distance between the markers.
    float journeyLength;

    // Time when the movement started.
    float startTime;

    private void Awake()
    {
        player = GameObject.Find("Player");
        fpsController = player.GetComponent<FpsControllerLPFP>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerCollider = player.GetComponent<Collider>();
    }
    /*private void Start()
    {
        player = GameObject.Find("Player");
        fpsController = player.GetComponent<FpsControllerLPFP>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerCollider = player.GetComponent<Collider>();
    }*/

    public override void OnUseItem() {
        
        if(sleepCoroutine == null)
            sleepCoroutine = StartCoroutine("MoveToBed");
    }
    IEnumerator MoveToBed()
    {
        Debug.Log("Clicked bed!");
        fpsController.enabled = false;
        playerRigidbody.isKinematic = true;
        playerCollider.enabled = false;
        startMarker = player.transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, sleepPosition.position);


        while (Vector3.Distance(player.transform.position, sleepPosition.position) > .01f)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            player.transform.position = Vector3.Lerp(startMarker.position, sleepPosition.position, fractionOfJourney);
            player.transform.rotation = Quaternion.Lerp(startMarker.rotation, sleepPosition.rotation, fractionOfJourney);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(5f);
        // TODO: Make screen dark
        // TODO: Play sound
        // TODO: Make screen light
        startTime = Time.time;
        journeyLength = Vector3.Distance(player.transform.position, wakeUpPosition.position);
        while (Vector3.Distance(player.transform.position, wakeUpPosition.position) > .01f)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            player.transform.position = Vector3.Lerp(player.transform.position, wakeUpPosition.position, fractionOfJourney);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, wakeUpPosition.rotation, fractionOfJourney);
            yield return new WaitForFixedUpdate();
        }

        fpsController.enabled = true;
        playerRigidbody.isKinematic = false;
        playerCollider.enabled = true;

        sleepCoroutine = null;
        yield return null;

        // TODO: Play sound
    }
    public void WakeUp()
    {
        sleepCoroutine = StartCoroutine("GetOutFromBed");
    }
    IEnumerator GetOutFromBed()
    {
        playerRigidbody.isKinematic = true;
        playerCollider.enabled = false;
        startMarker = player.transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(player.transform.position, wakeUpPosition.position);

        while (Vector3.Distance(player.transform.position, wakeUpPosition.position) > .01f)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            player.transform.position = Vector3.Lerp(player.transform.position, wakeUpPosition.position, fractionOfJourney);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, wakeUpPosition.rotation, fractionOfJourney);
            yield return new WaitForFixedUpdate();
        }
        fpsController.enabled = true;
        playerRigidbody.isKinematic = false;
        playerCollider.enabled = true;
        sleepCoroutine = null;
        yield return null;
    }
}
