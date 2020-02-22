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

    void Start()
    {
        // Vector3 start = Fight_Grid_Manager.Instance.PathVectors[0];
        //GameObject enemy = Instantiate(enemyPrefab, start, Quaternion.identity);
       // GameObject enemy = ObjectPooler.Instance.SpawnFromPool("Enemy", Fight_Grid_Manager.Instance.PathVectors[0], Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn)
        {
            // SPAWN
            GameObject enemy = ObjectPooler.Instance.Get("Peasant", fgm.PathVectors[0], Quaternion.identity);
            enemy.GetComponent<Peasant>().Index = 0;
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
