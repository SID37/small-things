using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListActivator : BaseActivator
{
    public List<BaseActivator> activators;

    void Start()
    {
    }

    void Update()
    {
        if (activated) return;
        foreach (var activator in activators)
        {
            if (!activator.activated)
                return;
        }
        activated = true;
    }
}
