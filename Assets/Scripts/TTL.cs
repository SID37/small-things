using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTL : MonoBehaviour
{
    public float ttl = 0.1f;

    void Start()
    {
        Destroy(gameObject, ttl);
    }
}
