using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActivator : MonoBehaviour
{
    public List<InputActivator> activators;
    public bool activated = false;

    void Start()
    {
    }

    void Update()
    {
        foreach (var activator in activators)
        {
            if (!activator.activated)
                return;
        }
        Debug.Log("=== ACTIVATED!!! ===");
        activated = true;
    }
}
