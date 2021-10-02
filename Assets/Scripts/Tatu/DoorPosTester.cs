using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPosTester : MonoBehaviour
{
    public GameObject testCube;
    public Renderer renderer;
    
    void Update()
    {
        float length = renderer.bounds.size.x;
        testCube.transform.position =this.transform.position;
    }
}
