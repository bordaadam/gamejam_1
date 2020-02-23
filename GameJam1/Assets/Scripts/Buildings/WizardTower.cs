using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTower : BuildingBase
{
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
}
