using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActivator : MonoBehaviour
{
    public bool activated = false;

    bool onPlayer = false;

    void Start()
    {
    }

    void Update()
    {
        if (!activated && onPlayer && Input.GetKeyDown(KeyCode.E))
            activated = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (activated) return;
        if (collider.GetComponent<PlayerController>() != null)
            onPlayer = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (activated) return;
        if (collider.GetComponent<PlayerController>() != null)
            onPlayer = false;
    }
}
