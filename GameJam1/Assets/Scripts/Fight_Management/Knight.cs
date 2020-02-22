using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : EnemyBase
{
    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        fgm = Fight_Grid_Manager.Instance;
    }

}
