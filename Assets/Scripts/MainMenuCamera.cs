using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] GameObject table;

    private void Start()
    {
        table = GameObject.Find("table");
    }

    void Update()
    {
        transform.LookAt(table.transform);
        transform.Translate(Vector3.right * Time.deltaTime * .1f);
    }
}
