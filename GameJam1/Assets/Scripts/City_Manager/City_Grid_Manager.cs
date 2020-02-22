using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Grid_Manager : MonoBehaviour
{
    public int x_size;
    public int y_size;

    [System.Serializable]
    public struct ResourceProperties{
        public string name;
        public float percentage;

        public Color color;
        public structureType type;

    }

    public ResourceProperties[] resProps;

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
        foreach(ResourceProperties rp in resProps)
        {
            int typeTileCount = (int)Mathf.Floor((float)x_size * (float)y_size * rp.percentage/100.0f);

            while(typeTileCount > 0)
            {
                GameObject gridCell = grids[random.Next(0,y_size),random.Next(0,x_size)];
                GameGrid gridCellType = gridCell.GetComponent<GameGrid>();
                if(gridCellType.structure == structureType.NOTHING)
                {
                    gridCellType.structure = rp.type;
                    gridCell.GetComponent<Renderer>().material.SetColor("_Color",rp.color);
                    typeTileCount--;
                }else
                {
                    continue;
                }
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
