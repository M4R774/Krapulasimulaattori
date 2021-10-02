using UnityEngine;
using System.Collections;

public class AmnesiaPickup : MonoBehaviour
{
    public Transform empty;
    public LayerMask interactLayer;
    [SerializeField] private float dist = 0.25f;

    [SerializeField] private Transform target;
    [SerializeField] Camera cam;

    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen

    void Update ()
    {
        Ray ray = cam.ViewportPointToRay(rayOrigin);//ScreenPointToRay(Input.mousePosition);

        RaycastHit h;
        //if(target == null)
        if(Physics.Raycast(transform.position, ray.direction, out h, 20f, interactLayer) && target == null) 
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                //if(Physics.Raycast(ray, ray.direction, out h, dist, interactLayer))
                //{
                    //Physics.Raycast(transform.position, ray.direction, out h, interactLayer);
                    //dist = Vector3.Distance(h.point, transform.position);
                    empty.position = transform.position+(ray.direction.normalized * dist);

                    target = h.transform;
                    Debug.Log(target);
                    //target = null;
                //}
            } 
        }

        Rigidbody rb = null;

        if(target != null)
            rb = target.GetComponent<Rigidbody>();

        if(Input.GetButton("Fire1") && rb)
        {
            //rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            target.transform.parent = empty;
            target.transform.localPosition = new Vector3 (0,0,0);
            empty.position = transform.position + (ray.direction.normalized * dist);
        }

        if(Input.GetButtonUp("Fire1") && rb)
        {
            //rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
            target.transform.parent = null;
            target = null;
        }
    }
}