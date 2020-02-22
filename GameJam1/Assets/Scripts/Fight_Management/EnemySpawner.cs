using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawns = 5f;
    private Fight_Grid_Manager fgm;
    private GameManager gm;
    private bool canSpawn = true;

    void Awake()
    {
        fgm = Fight_Grid_Manager.Instance;
        gm = GameManager.Instance;
    }

    void Start()
    {
        foreach(var cucc in fgm.PathVectors)
        {
            Debug.Log("Ide jövök: " + cucc);
        }
    }

    // csak akkor spawnoljunk ellenséget, ha remaining az > 0
    // ha megölünk vagy bemegy az ellenség, akkor remaining-- és az élet is....

    void Update()
    {
        if(canSpawn && gm.NeedToSpawn > 0)
        {
            // SPAWN
            gm.NeedToSpawn--;
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
