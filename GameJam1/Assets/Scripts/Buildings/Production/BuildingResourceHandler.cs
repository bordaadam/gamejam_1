using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum resourceType {WOOD,STONE,HUMAN,JAVELIN,RUNE}
public class BuildingResourceHandler : MonoBehaviour
{
    public float generateTime;
    public int amountGenerated;

    public resourceType myType;

    public Vector2 myPos;

    private float elapsedTime;
    private GameManager gameManager;
    private ProductionBuildingManager pbm;
    private City_Grid_Manager cgm;
    private Fight_Grid_Manager fgm;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gm = GameObject.Find("Game_Manager");
        gameManager = gm.GetComponent<GameManager>();
        pbm = gm.GetComponent<ProductionBuildingManager>();
        try{
            cgm = gm.GetComponent<City_Grid_Manager>();
            fgm = null;
        }catch
        {
            fgm = gm.GetComponent<Fight_Grid_Manager>();
            cgm = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= generateTime)
        {
            if(cgm != null)
            {
                if(pbm.CanReachPortal(cgm.grids,(int)myPos.x,(int)myPos.y))
                {
                    switch(myType)
                    {
                        case resourceType.WOOD: gameManager.Wood += amountGenerated;elapsedTime =0;break;
                        case resourceType.STONE: gameManager.Stone += amountGenerated;elapsedTime =0;break;
                        case resourceType.RUNE: gameManager.Runes += amountGenerated; elapsedTime =0;break;
                        case resourceType.JAVELIN: gameManager.Javelin += amountGenerated; elapsedTime =0;break;
                        case resourceType.HUMAN: gameManager.HumanResources +=  amountGenerated; elapsedTime =0;break;
                    }
                }
            }
        }
    }
}
