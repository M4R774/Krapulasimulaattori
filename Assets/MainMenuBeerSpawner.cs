using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBeerSpawner : MonoBehaviour
{
    public GameObject beerBottlePrefab;
    public float nextSpawn;
    public float spawnInterval = 5f;

    private void Start()
    {
        nextSpawn = Time.time + spawnInterval;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > nextSpawn)
        {
            GameObject beerBottle = Instantiate(beerBottlePrefab, transform);
            Destroy(beerBottle, 120f);
            nextSpawn = Time.time + spawnInterval;
        }
    }
}
