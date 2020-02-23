using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Grid_Manager : MonoBehaviour
{
    public GameObject[,] grids;

    public GameObject portal;
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private int wideness, depth;
    [SerializeField] private Color pathColor;
    [SerializeField] private Material[] grassVariants;

    public int GetWideness()
    {
        return wideness;
    }
    public int GetDepth()
    {
        return depth;
    }
    private GameObject parent;
    private static Fight_Grid_Manager _instance;

    public List<Vector3> PathVectors
    {
        get;
        private set;
    }

    public static Fight_Grid_Manager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }

        PathVectors = new List<Vector3>();
    }

    void Start()
    {
        parent = GameObject.Find("FightLevel");
        grids = GenerateGrids(wideness, depth);
        GeneratePath();
        //PutGrass();
    }

    private GameObject[,] GenerateGrids(int w, int h) 
    {
        GameObject[,] tmp = new GameObject[wideness, depth];

        for(int i = 0; i < w; i++)
        {
            for(int j = 0; j < h; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(i, 0f, j), Quaternion.identity); // actual grid
                obj.transform.parent = parent.transform;
                int randomGrass = UnityEngine.Random.Range(0, 3); // [0-3]
                obj.GetComponent<MeshRenderer>().material = grassVariants[randomGrass];
                
                
                obj.GetComponent<GameGrid>().pos = new Vector2(i, j);
                obj.GetComponent<GameGrid>().structure = structureType.TOWER_BUILD_GRID;
                obj.GetComponent<GameGrid>().my_fgm = this;
                obj.GetComponent<GameGrid>().my_cgm = null;

                // Grass
                tmp[i,j] = obj;


            }
        }

        return tmp;
    }

    private void GeneratePath()
    {
        Debug.Log($"A tömb: {grids.GetLength(0)},{grids.GetLength(1)}");
        int startX = 0, startY = 0;
        int endX = grids.GetLength(0)-1, endY = grids.GetLength(1)-1;
        Debug.Log($"startX: {startX}, startY: {startY}, endX: {endX}, endY: {endY}");
        MarkAsPath(startX, startY);
        PathVectors.Add(new Vector3(startX, -1, startY));

        int xDiff = (int)Mathf.Abs(startX - endX);
        int yDiff = (int)Mathf.Abs(endY - startY);
        int currentX = startX, currentY = startY;

        Debug.Log($"xDiff: {xDiff}, yDiff: {yDiff}");
        Debug.Log($"currentX: {currentX}, currentY: {currentY}");

        while((currentX != endX) || (currentY != endY))
        {
            int random = UnityEngine.Random.Range(0, 2);
            Debug.Log("Erre megy a random: " + random);

            if(random == 0)
            {
                // tehát x-et sorsoltuk
                if(xDiff > 0)
                {
                    //ha mehetünk még x irányába, akkor menjünk:
                    xDiff--;
                    currentX++;
                    MarkAsPath(currentX, currentY);
                }
            } else
            {
                if (yDiff > 0)
                {
                    //ha mehetünk még x irányába, akkor menjünk:
                    yDiff--;
                    currentY++;
                    MarkAsPath(currentX, currentY);
                }
            }
        }

        GameObject portalInstantiated = Instantiate(portal,grids[wideness-1,depth-1].transform.position + new Vector3(0f,0.75f,0f),Quaternion.identity);
        grids[wideness-1,depth-1].GetComponent<GameGrid>().structure = structureType.PORTAL;
        portalInstantiated.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        portalInstantiated.transform.GetChild(2).localScale = new Vector3(0.1f,0.1f,0.1f);
        if(grids[wideness-2,depth-1].transform.position.y == 0)
        {
            portalInstantiated.transform.rotation = Quaternion.Euler(new Vector3(0f,90f,0f));
        }
        grids[wideness-1,depth-1].GetComponent<GameGrid>().objectsHeld[1] = portalInstantiated;

    }

    private void MarkAsPath(int i, int j)
    {
        grids[i, j].GetComponent<Renderer>().material.color = pathColor;
        grids[i, j].GetComponent<GameGrid>().structure = structureType.PATH;
        grids[i, j].transform.position = new Vector3(grids[i, j].transform.position.x, grids[i, j].transform.position.y - 1, grids[i, j].transform.position.z);
        Vector3 trans = new Vector3(grids[i, j].transform.position.x, -1, grids[i, j].transform.position.y);
        PathVectors.Add(grids[i, j].transform.position);
    }

}
