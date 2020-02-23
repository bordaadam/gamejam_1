using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuildingManager : MonoBehaviour
{
    class SearchNode
    {
        public int posX,posY;
        public bool isVisited;
        public bool isSearchable;
        public SearchNode(int posX,int posY,bool isSearchable)
        {
            this.posX = posX;
            this.posY = posY;
            this.isVisited = false;
            this.isSearchable = isSearchable;
        }
    }
    SearchNode[,] graph;

    public bool CanReachPortal(GameObject[,] grid,int startX,int startY)
    {
        int size_i = grid.GetLength(0);
        int size_j = grid.GetLength(1);
        graph = new SearchNode[size_i,size_j];
        for(int i = 0; i < size_i; i++)
        {
            for(int j = 0; j < size_j;j++)
            {
                if(grid[i,j].GetComponent<GameGrid>().structure != structureType.NOTHING && grid[i,j].GetComponent<GameGrid>().structure != structureType.WOOD && grid[i,j].GetComponent<GameGrid>().structure != structureType.STONE && grid[i,j].GetComponent<GameGrid>().structure != structureType.INNOCENT_HUMAN)
                {
                    graph[i,j] = new SearchNode(i,j,true);
                }else
                {
                    graph[i,j] = new SearchNode(i,j,false);
                }    
            }
        }

        Queue<SearchNode> queue = new Queue<SearchNode>();
        graph[startX,startY].isVisited = true;
        queue.Enqueue(graph[startX,startY]);
        SearchNode current;
        while(queue.Count > 0)
        {
            current = queue.Dequeue();
            //print current
            Debug.Log(grid[current.posX,current.posY].GetComponent<GameGrid>().structure);
            if(grid[current.posX,current.posY].GetComponent<GameGrid>().structure == structureType.PORTAL)
            {
                return true;
            }

            if(current.posX + 1 < size_i)
            {
                if(graph[current.posX +1 , current.posY].isSearchable && !graph[current.posX +1 , current.posY].isVisited)
                {
                    graph[current.posX +1 , current.posY].isVisited = true;
                    queue.Enqueue(graph[current.posX +1 , current.posY]);
                }
            }
            if(current.posX -1 >= 0)
            {
                if(graph[current.posX -1 , current.posY].isSearchable && !graph[current.posX -1 , current.posY].isVisited)
                {
                    graph[current.posX -1 , current.posY].isVisited = true;
                    queue.Enqueue(graph[current.posX -1 , current.posY]);
                }
            }
            if(current.posY + 1 < size_j)
            {
                if(graph[current.posX , current.posY+1].isSearchable && !graph[current.posX , current.posY+1].isVisited)
                {
                    graph[current.posX , current.posY+1].isVisited = true;
                    queue.Enqueue(graph[current.posX , current.posY+1]);
                }
            }
            if(current.posY - 1 >= 0)
            {
                if(graph[current.posX , current.posY-1].isSearchable && !graph[current.posX , current.posY-1].isVisited)
                {
                    graph[current.posX , current.posY-1].isVisited = true;
                    queue.Enqueue(graph[current.posX , current.posY-1]);
                }
            }
        }
        return false;
    }
}
