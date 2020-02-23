using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTower : BuildingBase
{

    private AudioSource source;

    protected override void PlaySound()
    {
        source.Play();
    }

    protected override void TakeResource()
    {
        GameManager.Instance.Runes--;
    }

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        source = GetComponent<AudioSource>();
    }
}
