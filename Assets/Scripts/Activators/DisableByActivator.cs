using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableByActivator : MonoBehaviour
{
    public GameObject target;
    public BaseActivator activator;

    void Start()
    {
        target.gameObject.SetActive(true);
    }

    void Update()
    {
        if (activator.activated)
        {
            target.gameObject.SetActive(false);
        }
    }
}
