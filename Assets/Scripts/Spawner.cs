using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public PlayerController player;
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
        if (!player.enabled) enabled = false;
        if (timeToSpawn <= 0 && (transform.position - player.transform.position).magnitude > 4) {
            timeToSpawn = interval;
            var thing = Instantiate(spawnObject);
            thing .transform.position = transform.position;
            thing.player = player;
        }
    }
}
