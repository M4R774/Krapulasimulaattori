using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody rigid_body = other.GetComponent<Rigidbody>();
            rigid_body.isKinematic = true;
            other.transform.position = new Vector3(0.1f, 1.0f, 0.6f);
            rigid_body.isKinematic = false;
        }
    }
}
