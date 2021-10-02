using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public abstract class AI : MonoBehaviour
{
    enum Status
    {
        idle,
        chase,
        returnHome,
        investigate
    }
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] float speed = 2.5f;
    [SerializeField] LayerMask layerMask;
    public Vector3 origPos;
    [SerializeField] Status status;
    Coroutine chaseCoolDown;
    private Quaternion lookRotation;
    private Vector3 direction;
    [SerializeField] float collisionSoundNoticeThreshold;

    void Start()
    {
        origPos = transform.position;
        GameEvents.current.onCollisionSound += OnCollisionSound;
    }

    void Update()
    {
        float step =  speed * Time.deltaTime;

        switch(status)
        {
            case Status.idle:
            target = null;
            break;

            case Status.chase:
            agent.SetDestination(target.position);
            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            //transform.LookAt(target);
            break;

            case Status.returnHome:
            target = null;
            //transform.position = Vector3.MoveTowards(transform.position, origPos, step);
            agent.SetDestination(origPos);
            /*direction = (origPos - transform.position).normalized; // find the vector pointing from our position to the target
            lookRotation = Quaternion.LookRotation(direction); // create the rotation we need to be in to look at the target
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, step); // rotate us over time according to speed until we are in the required rotation*/
            break;

            case Status.investigate:
            if(chaseCoolDown != null)
                StopCoroutine(chaseCoolDown);
            agent.SetDestination(target.position);
            break;
        }
        if(status == Status.returnHome)
        {
            if((transform.position-origPos).sqrMagnitude<0.25f*0.25f) // when AI is within 0.25x0.25 of its original position
            {
                status = Status.idle;
            }
        }
        if(status == Status.investigate)
        {
            if(chaseCoolDown != null)
                StopCoroutine(chaseCoolDown);
            
            chaseCoolDown = StartCoroutine(ChaseCoolDown(5f));
        }
    }

    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        //int layerMask = 1 << 11;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if(hit.collider.gameObject.tag == "Player")
            {
                if(chaseCoolDown != null)
                    StopCoroutine(chaseCoolDown);
                chaseCoolDown = null;
                target = hit.transform;
                status = Status.chase;
            }
            else if(status != Status.investigate)
            {
                if(chaseCoolDown == null)
                    chaseCoolDown = StartCoroutine(ChaseCoolDown(5f));
            }
        }
    }

    IEnumerator ChaseCoolDown(float searchTime)
    {
        Debug.Log("Started cool down");
        yield return new WaitForSeconds(searchTime);
        status = Status.returnHome;
        chaseCoolDown = null;
        Debug.Log("Stopped cool down");
        yield return null;
    }

    private void OnCollisionSound(Vector3 force, Transform impactTransform)
    {
        float[] forces = {force.x, force.y, force.z};
        foreach(var forceValue in forces)
        {
            if(forceValue > collisionSoundNoticeThreshold)
            {
                status = Status.investigate;
                target = impactTransform;
                //Debug.Log(impactTransform.gameObject);
            }
        }
    }

    void OnDestroy()
    {
        GameEvents.current.onCollisionSound -= OnCollisionSound;
    }
}
