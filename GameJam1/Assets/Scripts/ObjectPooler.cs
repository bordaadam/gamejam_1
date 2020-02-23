using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public static ObjectPooler Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    void Awake()
    {
        Instance = this;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject Get(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " does not exists");
            return null;
        }

        if(poolDictionary[tag].Count == 0)
        {
            IncreasePoolBy(tag, 20);
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        //objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    public void Put(string tag, GameObject go)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " does not exists");
            return;
        }

        go.SetActive(false);
        go.transform.position = new Vector3(0, 30, 0);

        poolDictionary[tag].Enqueue(go);
    }

    private void IncreasePoolBy(string tag, int number)
    {
        foreach(var pool in pools)
        {
            if(pool.tag == tag)
            {
                for(int i = 0; i < number; i++)
                {
                    GameObject go = Instantiate(pool.prefab);
                    go.SetActive(false);
                    poolDictionary[tag].Enqueue(go);
                }
            }
        }
    }

}
