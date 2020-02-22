﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum structureType {NOTHING, WOOD, WOOD_TOWER, WOOD_GATHERER, WOOD_PROCESSER, PATH}

public class GameGrid : MonoBehaviour
{
    public Vector2 pos;
    public structureType structure;
    public GameObject[] objectsHeld = new GameObject[2]; //objectsHeld[0] -> natural resources; objectsHeld[1] -> buildings (and path)
}
