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

    private const string KNIGHT = "Knight";
    private const string PEASANT = "Peasant";

    void Awake()
    {
        fgm = Fight_Grid_Manager.Instance;
        gm = GameManager.Instance;
    }

    // csak akkor spawnoljunk ellenséget, ha remaining az > 0
    // ha megölünk vagy bemegy az ellenség, akkor remaining-- és az élet is....

    void Update()
    {
        if(canSpawn && gm.NeedToSpawn > 0)
        {
            // SPAWN
            gm.NeedToSpawn--;
            SpawnRandom();
            StartCoroutine(Wait(timeBetweenSpawns));
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
