using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenTower : BuildingBase
{
   void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

}
