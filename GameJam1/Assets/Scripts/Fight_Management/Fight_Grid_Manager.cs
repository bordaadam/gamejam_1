using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Grid_Manager : MonoBehaviour
{
    private GameObject[,] grids;
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private int wideness, depth;
    [SerializeField] private Color pathColor;
    private GameObject parent;

    void Awake()
    {
        parent = GameObject.Find("FightLevel");
    }

    void Start()
    {
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
    }

}
