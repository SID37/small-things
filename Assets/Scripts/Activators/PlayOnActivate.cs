using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayOnActivate : MonoBehaviour
{
    public BaseActivator activator;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (source != null && activator.activated)
        {
            source.Play();
            source = null;
        }
    }
}
