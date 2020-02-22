using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Grid_Manager : MonoBehaviour
{
    public int x_size;
    public int y_size;

    public float woodPercentage;

    public Color woodColor;

    public GameObject grid;

    private GameObject[,] grids;
    private System.Random random = new System.Random();

    void MakeField()
    {
        for(int i = 0; i < x_size; i++)
        {
            for(int j = 0; j < y_size; j++)
            {
                grids[i,j] = Instantiate(grid, new Vector3(i,0f,j),Quaternion.identity);
                grids[i,j].GetComponent<Renderer>().material.SetColor("_Color",Color.green);
                grids[i,j].GetComponent<GameGrid>().pos = new Vector2(i,j);
                grids[i,j].GetComponent<GameGrid>().structure = structureType.NOTHING;
            }
        }
    }

    void InstallResources()
    {
        int woodTileCount = (int)Mathf.Floor((float)x_size * (float)y_size * woodPercentage/100.0f);

        while(woodTileCount > 0)
        {
            GameObject gridCell = grids[random.Next(0,y_size),random.Next(0,x_size)];
            GameGrid gridCellType = gridCell.GetComponent<GameGrid>();
            if(gridCellType.structure == structureType.NOTHING)
            {
                gridCellType.structure = structureType.WOOD;
                gridCell.GetComponent<Renderer>().material.SetColor("_Color",woodColor);
                woodTileCount--;
            }else
            {
                continue;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        grids = new GameObject[y_size,x_size];
        MakeField();
        InstallResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
