using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingTrap : BuildingBase
{
    protected override void PlaySound()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    void Update()
    {

    }

}
