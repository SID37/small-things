using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActivator : BaseActivator
{
    PlayerController player = null;

    void Start()
    {
    }

    void Update()
    {
        if (!activated && player != null && Input.GetKeyDown(KeyCode.E))
        {
            activated = true;
            player.Hint(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (activated) return;
        player = collider.GetComponent<PlayerController>();
        player?.Hint(true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (activated) return;
        if (player != null && collider.GetComponent<PlayerController>() != null)
        {
            player.Hint(false);
            player = null;
        }
    }
}
