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
        Vector3 start = Fight_Grid_Manager.Instance.PathVectors[0];
        GameObject enemy = Instantiate(enemyPrefab, start, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
