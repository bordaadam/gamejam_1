using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Grid_Manager : MonoBehaviour
{
    public int x_size;
    public int y_size;

    public int x_offset;
    public int y_offset;
    public Color baseColor;

    [System.Serializable]
    public struct ResourceProperties{
        public string name;
        public float percentage;

        public Color color;
        public structureType type;

    }

    public ResourceProperties[] resProps;

    public GameObject[] grassVariants;

    public GameObject grid;

    public GameObject[,] grids;
    private System.Random random = new System.Random();

    void MakeField()
    {
        float[] randomRotations = {0f,90f,180f,270f};
        for(int i = 0; i < x_size; i++)
        {
            for(int j = 0; j < y_size; j++)
            {
                grids[i,j] = Instantiate(grid, new Vector3(i+x_offset,0f,j+y_offset),Quaternion.identity);
                grids[i,j].GetComponent<Renderer>().material.SetColor("_Color",baseColor);
                grids[i,j].GetComponent<GameGrid>().pos = new Vector2(i,j);
                grids[i,j].GetComponent<GameGrid>().structure = structureType.NOTHING;
                grids[i,j].GetComponent<GameGrid>().my_cgm = this;
                grids[i,j].GetComponent<GameGrid>().my_fgm = null;
                //GameObject tmp = Instantiate(grassVariants[random.Next(grassVariants.Length)],grids[i,j].transform.position + new Vector3(0f,0.51f,0f), Quaternion.Euler(new Vector3(270f,0f, randomRotations[random.Next(randomRotations.Length)])));
                //grids[i,j].GetComponent<GameGrid>().objectsHeld[0] = tmp;
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
                GameObject gridCell = grids[random.Next(0,x_size),random.Next(0,y_size)];
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
        grids = new GameObject[x_size,y_size];
        MakeField();
        InstallResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
