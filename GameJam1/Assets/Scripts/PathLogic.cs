using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLogic : MonoBehaviour
{
    public Vector2 pos;
    public GameObject myGrid;
    

    public void UpdateNeighbours()
    {
        int numOfNeighbours = 0;
        Debug.Log(pos.ToString());
        if(myGrid.GetComponent<GameGrid>().my_cgm != null)
        {
            int xSize = myGrid.GetComponent<GameGrid>().my_cgm.x_size;
            int ySize = myGrid.GetComponent<GameGrid>().my_cgm.y_size;
            bool[] neighbours = new bool[4];//+x,-x,+y,-y

            if(pos.x + 1 < xSize)
            {
                if(myGrid.GetComponent<GameGrid>().my_cgm.grids[(int)pos.x+1,(int)pos.y].GetComponent<GameGrid>().objectsHeld[1] != null)
                {
                    if(myGrid.GetComponent<GameGrid>().my_cgm.grids[(int)pos.x+1,(int)pos.y].GetComponent<GameGrid>().objectsHeld[1].tag == "Path")
                    {
                        numOfNeighbours++;
                        neighbours[0] = true;
                    }else
                    {
                        neighbours[0] = false;
                    }
                }
            }

            if(pos.x - 1 >= 0)
            {
                if(myGrid.GetComponent<GameGrid>().my_cgm.grids[(int)pos.x-1,(int)pos.y].GetComponent<GameGrid>().objectsHeld[1] != null)
                {
                    if(myGrid.GetComponent<GameGrid>().my_cgm.grids[(int)pos.x-1,(int)pos.y].GetComponent<GameGrid>().objectsHeld[1].tag == "Path")
                    {
                        numOfNeighbours++;
                        neighbours[1] = true;
                    }else
                    {
                        neighbours[1] = false;
                    }
                }
            }

            if(pos.y + 1 < ySize)
            {
                //Debug.Log((int)pos.x + " >> " + ((int)pos.y+1) + ">> " +myGrid.GetComponent<GameGrid>().my_cgm.grids[(int)pos.x,(int)pos.y+1].GetComponent<GameGrid>().pos.ToString());
                if(myGrid.GetComponent<GameGrid>().my_cgm.grids[(int)pos.x,(int)pos.y+1].GetComponent<GameGrid>().objectsHeld[1] != null)
                {
                    Debug.Log("Found The Path");
                    if(myGrid.GetComponent<GameGrid>().my_cgm.grids[(int)pos.x,(int)pos.y+1].GetComponent<GameGrid>().objectsHeld[1].tag == "Path")
                    {
                        numOfNeighbours++;
                        neighbours[2] = true;
                        Debug.Log("Found The Tag");
                    }else
                    {
                        neighbours[2] = false;
                    }
                }
            }

            if(pos.y - 1 >= 0)
            {
                if(myGrid.GetComponent<GameGrid>().my_cgm.grids[(int)pos.x,(int)pos.y-1].GetComponent<GameGrid>().objectsHeld[1] != null)
                {
                    Debug.Log("Found The Path");
                    if(myGrid.GetComponent<GameGrid>().my_cgm.grids[(int)pos.x,(int)pos.y-1].GetComponent<GameGrid>().objectsHeld[1].tag == "Path")
                    {
                        numOfNeighbours++;
                        neighbours[3] = true;
                        Debug.Log("Found The Tag");
                    }else
                    {
                        neighbours[3] = false;
                    }
                }
            }

            if(numOfNeighbours > 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[6];//4-way
            }else if(numOfNeighbours == 2)
            {
                if(neighbours[0] && neighbours[1])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[0];//horizontal
                }
                if(neighbours[0] && neighbours[2])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[3];//left-to-down
                }
                if(neighbours[0] && neighbours[3])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[2];//left- to-up
                }
                if(neighbours[1] && neighbours[2])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[4];//right-to-down
                }
                if(neighbours[1] && neighbours[3])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[5];//right-to-up
                }
                if(neighbours[2] && neighbours[3])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[1];//up-down
                }
            }else if(numOfNeighbours == 1)
            {
                if(neighbours[0] || neighbours[1])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[0];
                }
                if(neighbours[2] || neighbours[3])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[1];
                }
            }


        }else if(myGrid.GetComponent<GameGrid>().my_fgm != null) //EOF cgm
        {
            int xSize = myGrid.GetComponent<GameGrid>().my_fgm.GetWideness();
            int ySize = myGrid.GetComponent<GameGrid>().my_fgm.GetDepth();
            bool[] neighbours = new bool[4];//+x,-x,+y,-y

            if(pos.x + 1 < xSize)
            {
                if(myGrid.GetComponent<GameGrid>().my_fgm.grids[(int)pos.x+1,(int)pos.y].GetComponent<GameGrid>().objectsHeld[1] != null)
                {
                    if(myGrid.GetComponent<GameGrid>().my_fgm.grids[(int)pos.x+1,(int)pos.y].GetComponent<GameGrid>().objectsHeld[1].tag == "Path")
                    {
                        numOfNeighbours++;
                        neighbours[0] = true;
                    }else
                    {
                        neighbours[0] = false;
                    }
                }
            }

            if(pos.x - 1 >= 0)
            {
                if(myGrid.GetComponent<GameGrid>().my_fgm.grids[(int)pos.x-1,(int)pos.y].GetComponent<GameGrid>().objectsHeld[1] != null)
                {
                    if(myGrid.GetComponent<GameGrid>().my_fgm.grids[(int)pos.x-1,(int)pos.y].GetComponent<GameGrid>().objectsHeld[1].tag == "Path")
                    {
                        numOfNeighbours++;
                        neighbours[1] = true;
                    }else
                    {
                        neighbours[1] = false;
                    }
                }
            }

            if(pos.y + 1 < ySize)
            {
                if(myGrid.GetComponent<GameGrid>().my_fgm.grids[(int)pos.x,(int)pos.y+1].GetComponent<GameGrid>().objectsHeld[1] != null)
                {
                    if(myGrid.GetComponent<GameGrid>().my_fgm.grids[(int)pos.x,(int)pos.y+1].GetComponent<GameGrid>().objectsHeld[1].tag == "Path")
                    {
                        numOfNeighbours++;
                        neighbours[2] = true;
                    }else
                    {
                        neighbours[2] = false;
                    }
                }
            }

            if(pos.y - 1 >= 0)
            {
                if(myGrid.GetComponent<GameGrid>().my_fgm.grids[(int)pos.x,(int)pos.y-1].GetComponent<GameGrid>().objectsHeld[1] != null)
                {
                    if(myGrid.GetComponent<GameGrid>().my_fgm.grids[(int)pos.x,(int)pos.y-1].GetComponent<GameGrid>().objectsHeld[1].tag == "Path")
                    {
                        numOfNeighbours++;
                        neighbours[3] = true;
                    }else
                    {
                        neighbours[3] = false;
                    }
                }
            }
            if(numOfNeighbours > 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[6];//4-way
            }else if(numOfNeighbours == 2)
            {
                if(neighbours[0] && neighbours[1])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[0];//horizontal
                }
                if(neighbours[0] && neighbours[2])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[3];//left-to-down
                }
                if(neighbours[0] && neighbours[3])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[2];//left- to-up
                }
                if(neighbours[1] && neighbours[2])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[4];//right-to-down
                }
                if(neighbours[1] && neighbours[3])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[5];//right-to-up
                }
                if(neighbours[2] && neighbours[3])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[1];//up-down
                }
            }else if(numOfNeighbours == 1)
            {
                if(neighbours[0] || neighbours[1])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[0];
                }
                if(neighbours[2] || neighbours[3])
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = myGrid.GetComponent<GameGrid>().my_cgm.gameObject.GetComponent<PathManager>().roads[1];
                }
            }
        }
    }
}
