using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] GameObject table;
    [SerializeField] bool rotateCamera = false;

    private void Start()
    {
        //table = GameObject.Find("table");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if(rotateCamera)
        {
            transform.LookAt(table.transform);
            transform.Translate(Vector3.right * Time.deltaTime * .1f);
        }
        //Cursor.visible = true;
    }
}
