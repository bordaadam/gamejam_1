﻿using System.Collections;
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

        public GameObject sprite;

    }

    public ResourceProperties[] resProps;

    public GameObject[] grassVariants;

    public GameObject grid;

    public GameObject[,] grids;
    private System.Random random = new System.Random();

    public GameObject portal;

    private GameObject portalInstantiated;

    void MakeField()
    {
       
        for (int i = 0; i < x_size; i++)
        {
            for(int j = 0; j < y_size; j++)
            {
                grids[i,j] = Instantiate(grid, new Vector3(i+x_offset,0f,j+y_offset),Quaternion.identity);
                grids[i,j].GetComponent<Renderer>().material.SetColor("_Color",baseColor);
                grids[i,j].GetComponent<GameGrid>().pos = new Vector2(i,j);
                grids[i,j].GetComponent<GameGrid>().structure = structureType.NOTHING;
                grids[i,j].GetComponent<GameGrid>().my_cgm = this;
                grids[i,j].GetComponent<GameGrid>().my_fgm = null;
                if(i == 0 && j == 0)
                {
                    portalInstantiated = Instantiate(portal,grids[i,j].transform.position + new Vector3(0f,1.25f,0f),Quaternion.identity);
                    Debug.Log("EZ:" + portalInstantiated.transform.GetChild(2));
                    grids[i,j].GetComponent<GameGrid>().structure = structureType.PORTAL;
                    portalInstantiated.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
                    portalInstantiated.transform.GetChild(2).localScale = new Vector3(0.3f,0.3f,0.3f);
                    portalInstantiated.transform.rotation = Quaternion.Euler(new Vector3(0f,90f,0f));
                    grids[i,j].GetComponent<GameGrid>().objectsHeld[1] = portalInstantiated;
                }
                GameObject tmp = Instantiate(grassVariants[random.Next(grassVariants.Length)],grids[i,j].transform.position + new Vector3(0f,0.51f,0f), Quaternion.Euler(new Vector3(270f,0f,0f)));
                tmp.transform.localScale = new Vector3(0.78f,0.78f,1f);
                grids[i,j].GetComponent<GameGrid>().objectsHeld[0] = tmp;
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
                    //gridCell.GetComponent<Renderer>().material.SetColor("_Color",rp.color);
                    GameObject.Destroy(gridCellType.objectsHeld[0]);
                    GameObject tmp = Instantiate(rp.sprite,gridCell.transform.position + new Vector3(0f,0.51f,0f), Quaternion.Euler(new Vector3(270f,0f,0f)));
                    tmp.transform.localScale = new Vector3(0.78f,0.78f,1f);
                    gridCellType.objectsHeld[0] = tmp;
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
        x_size = GameObject.Find("MenuMaster").GetComponent<MenuControls>().width;
        y_size = GameObject.Find("MenuMaster").GetComponent<MenuControls>().height;
        grids = new GameObject[x_size,y_size];
        MakeField();
        InstallResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
