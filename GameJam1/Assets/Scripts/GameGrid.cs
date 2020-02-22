using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum structureType {NOTHING, WOOD, WOOD_TOWER, WOOD_GATHERER, WOOD_PROCESSER, PATH,TOWER_BUILD_GRID,STONE,INNOCENT_HUMAN,STONE_GATHERER,STONE_PROCESSOR}

public class GameGrid : MonoBehaviour
{
    public Vector2 pos;
    public structureType structure;
    public GameObject[] objectsHeld = new GameObject[2]; //objectsHeld[0] -> natural resources; objectsHeld[1] -> buildings (and path)
    public City_Grid_Manager my_cgm; //Null if Fighter grid
    public Fight_Grid_Manager my_fgm; //Null if City grid
}
