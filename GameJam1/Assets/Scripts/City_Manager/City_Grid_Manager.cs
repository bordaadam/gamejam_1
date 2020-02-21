using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Grid_Manager : MonoBehaviour
{
    public int x_size;
    public int y_size;

    public GameObject grid;

    private GameObject[,] grids;

    void MakeField()
    {
        for(int i = 0; i < y_size; i++)
        {
            for(int j = 0; j < x_size; j++)
            {
                grids[i,j] = Instantiate(grid, new Vector3(i,0f,j),Quaternion.identity);
                grids[i,j].GetComponent<GameGrid>().pos = new Vector2(j,i);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        grids = new GameObject[y_size,x_size];
        MakeField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
