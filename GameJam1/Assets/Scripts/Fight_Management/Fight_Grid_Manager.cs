using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Grid_Manager : MonoBehaviour
{
    private GameObject[,] grids;
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private int depth, wideness;
    private GameObject parent;

    void Awake()
    {
        parent = GameObject.Find("FightLevel");
    }

    void Start()
    {
        grids = GenerateGrids(depth, wideness);
        parent = GameObject.Find("FightLevel");
    }

    private GameObject[,] GenerateGrids(int depth, int wideness) 
    {
        GameObject[,] tmp = new GameObject[depth, wideness];

        for(int i = 0; i < depth; i++)
        {
            for(int j = 0; j < wideness; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(i, 0f, j), Quaternion.identity); // actual grid
                obj.transform.parent = parent.transform;
                
                obj.GetComponent<GameGrid>().pos = new Vector2(i, j);
                tmp[i,j] = obj;
            }
        }

        return tmp;
    }

}
