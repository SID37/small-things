using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableByActivator : MonoBehaviour
{
    public MonoBehaviour target;
    public BaseActivator activator;

    void Start()
    {
        target.gameObject.SetActive(false);
    }

    void Update()
    {
        if (activator.activated)
        {
            target.gameObject.SetActive(true);
        }
    }
}
