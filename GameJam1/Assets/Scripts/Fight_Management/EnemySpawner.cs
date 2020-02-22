using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawns = 5f;
    private Fight_Grid_Manager fgm;
    private bool canSpawn = true;

    void Awake()
    {
        fgm = Fight_Grid_Manager.Instance;
    }

    void Update()
    {
        if(canSpawn)
        {
            // SPAWN
            GameObject enemy = ObjectPooler.Instance.Get("Knight", fgm.PathVectors[0], Quaternion.identity);
            enemy.GetComponent<Knight>().Index = 0;
            StartCoroutine(Wait(timeBetweenSpawns));
        }
    }

    IEnumerator Wait(float time)
    {
        canSpawn = false;
        yield return new WaitForSeconds(time);
        canSpawn = true;
    }
}
