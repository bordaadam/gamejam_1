using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Vector3 start = Fight_Grid_Manager.Instance.PathVectors[0];
        //GameObject enemy = Instantiate(enemyPrefab, start, Quaternion.identity);
       // GameObject enemy = ObjectPooler.Instance.SpawnFromPool("Enemy", Fight_Grid_Manager.Instance.PathVectors[0], Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject enemy = ObjectPooler.Instance.Get("Enemy", Fight_Grid_Manager.Instance.PathVectors[0], Quaternion.identity);
            enemy.GetComponent<Enemy>().Index = 0;
        }
    }

    void FixedUpdate()
    {
    }
}
