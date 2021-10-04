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
        Cursor.visible = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > nextSpawn)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
            GameObject beerBottle = Instantiate(beerBottlePrefab, transform);
            beerBottle.transform.position += randomOffset;
            Destroy(beerBottle, 120f);
            nextSpawn = Time.time + spawnInterval;
        }
    }
}
