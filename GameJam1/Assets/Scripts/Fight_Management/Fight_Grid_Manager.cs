using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Grid_Manager : MonoBehaviour
{
    public GameObject[,] grids;
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private int wideness, depth;
    [SerializeField] private Color pathColor;

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
                
                
                obj.GetComponent<GameGrid>().pos = new Vector2(i, j);
                obj.GetComponent<GameGrid>().structure = structureType.NOTHING;
                obj.GetComponent<GameGrid>().my_fgm = this;
                obj.GetComponent<GameGrid>().my_cgm = null;
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
            int random = Random.Range(0, 2);
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
