using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform player;
    public ThingController spawnObject;
    public float interval = 1.0f;

    private float timeToSpawn;


    void Start()
    {
        timeToSpawn = Random.Range(0.0f, interval);
    }

    void Update()
    {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0) {
            timeToSpawn = interval;
            var thing = Instantiate(spawnObject);
            thing .transform.position = transform.position;
            thing.player = player;
        }
    }
}
