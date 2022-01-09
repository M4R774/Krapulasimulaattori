using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] GameObject table;
    [SerializeField] bool rotateCamera = false;
    bool moveRight = true;
    bool moveLeft = false;

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
            if(transform.localEulerAngles.y > 35 && transform.localEulerAngles.y < 38)
            {
                moveRight = true;
                moveLeft = false;
            }
            else if(transform.localEulerAngles.y < 325 && transform.localEulerAngles.y > 322)
            {
                moveRight = false;
                moveLeft = true;
            }
            if(moveRight)
                transform.Translate(Vector3.right * Time.deltaTime * .1f);
            else if(moveLeft)
                transform.Translate(Vector3.left * Time.deltaTime * .1f);
        }
        //Cursor.visible = true;
    }

    /*
    Orig camera rotate by Aleksi
    transform.LookAt(table.transform);
    transform.Translate(Vector3.right * Time.deltaTime * .1f);
    */
}
