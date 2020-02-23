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
    private bool firstSpawn = false;

    [SerializeField] private float timeToFirstSpawn = 10f;

    void Start()
    {
        fgm = Fight_Grid_Manager.Instance;
        gm = GameManager.Instance;
        StartCoroutine(First(timeToFirstSpawn));
    }

    IEnumerator First(float time)
    {
        yield return new WaitForSeconds(time);
        firstSpawn = true;
    }

    void Update()
    {
        if(firstSpawn)
        {
            if(canSpawn && gm.NeedToSpawn > 0)
            {
                Debug.Log("Spawnolok enemyt!");
                // SPAWN
                gm.NeedToSpawn--;
                SpawnRandom();
                StartCoroutine(Wait(timeBetweenSpawns));
            }
        }
    }

    private void SpawnRandom()
    {
        // TODO: máshogyan sorsoljon ellenfeleket!

        int random = Random.Range(0, 2);
        switch(random)
        {
            case 0:
                GameObject enemy = ObjectPooler.Instance.Get("Knight", fgm.PathVectors[0], Quaternion.identity);
                enemy.GetComponent<Knight>().Index = 0;
                break;
            case 1:
                GameObject enemy2 = ObjectPooler.Instance.Get("Peasant", fgm.PathVectors[0], Quaternion.identity);
                enemy2.GetComponent<Peasant>().Index = 0;
                break;
        }
    }

    IEnumerator Wait(float time)
    {
        canSpawn = false;
        yield return new WaitForSeconds(time);
        canSpawn = true;
    }
}
