using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class WoodenTower : BuildingBase
{

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider>();
    }

    protected override void PlaySound()
    {
        source.Play();
    }

}
