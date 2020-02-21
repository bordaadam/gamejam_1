using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum structureType {NOTHING, WOOD, WOOD_TOWER, WOOD_GATHERER, WOOD_PROCESSER, PATH}

public class Grid : MonoBehaviour
{
    public Vector2 pos;
    public structureType structure;
}
