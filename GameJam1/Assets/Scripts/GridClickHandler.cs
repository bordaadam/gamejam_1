using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClickHandler : MonoBehaviour
{
    private UI_Handler uiHandler;
    private GameObject ghost;
    private Color ghostOriginalColor;

    public void InstantiateGhost()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2f;       //we want min distance from the camera
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        ghost = Instantiate(uiHandler.getCurrentlySelectedModell().modell, objectPos, Quaternion.Euler(uiHandler.getCurrentlySelectedModell().instantiateRotation));
        ghost.transform.localScale = uiHandler.getCurrentlySelectedModell().instantiateScale;
        ghostOriginalColor = ghost.transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_Color");
    }
    private void Start() {
        uiHandler = gameObject.GetComponent<UI_Handler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(uiHandler.isBuildingMode)
        {      
            RaycastHit  hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 vect = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2f);
            ghost.transform.position =  Camera.main.ScreenToWorldPoint(vect);
            if (Physics.Raycast(ray, out hit)) //handle ghost
            {
                GameObject hitGO = hit.transform.gameObject;
                try{//We expect that the hit object has GameGrid component
                    if(hitGO.GetComponent<GameGrid>().structure == uiHandler.getCurrentlySelectedModell().canBeBuiltOn)
                    {
                        ghost.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color",ghostOriginalColor);
                        ghost.transform.position = hit.transform.position + uiHandler.getCurrentlySelectedModell().instantiateOffset;
                    }else
                    {
                        ghost.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color",Color.red);
                        ghost.transform.position = hit.transform.position + uiHandler.getCurrentlySelectedModell().instantiateOffset;
                    }
                }catch{

                }
            }else
            {
                ghost.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color",Color.red);
                ghost.transform.position =  Camera.main.ScreenToWorldPoint(vect);
            }
            if(Input.GetMouseButtonUp(1))//cancel building mode
            {
                GameObject.Destroy(ghost);
                uiHandler.isBuildingMode = false;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit  hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                  
            if (Physics.Raycast(ray, out hit))
            {
                try //We expect to have a grid component attached to the hit object
                {
                    if(uiHandler.isBuildingMode && hit.transform.gameObject.GetComponent<GameGrid>().structure == uiHandler.getCurrentlySelectedModell().canBeBuiltOn)
                    {
                        GameObject.Destroy(ghost);
                        uiHandler.isBuildingMode = false;
                        hit.transform.gameObject.GetComponent<GameGrid>().structure = uiHandler.getCurrentlySelectedModell().ownType;
                        GameObject tmp = Instantiate(uiHandler.getCurrentlySelectedModell().modell,hit.transform.position + uiHandler.getCurrentlySelectedModell().instantiateOffset,Quaternion.identity);
                        tmp.transform.rotation = Quaternion.Euler(uiHandler.getCurrentlySelectedModell().instantiateRotation);
                        tmp.transform.localScale = uiHandler.getCurrentlySelectedModell().instantiateScale;
                        hit.transform.gameObject.GetComponent<GameGrid>().objectsHeld[1] = tmp;
                        //Debug.Log(hit.transform.gameObject.GetComponent<GameGrid>().pos.ToString() + " | " + hit.transform.gameObject.GetComponent<GameGrid>().objectsHeld[1].ToString());
                        if(uiHandler.getCurrentlySelectedModell().ownType == structureType.PATH)
                        {
                            gameObject.GetComponent<PathManager>().paths.Add(tmp);
                            tmp.transform.GetChild(0).GetComponent<PathLogic>().myGrid = hit.transform.gameObject;
                            tmp.transform.GetChild(0).GetComponent<PathLogic>().pos = hit.transform.gameObject.GetComponent<GameGrid>().pos;
                        }
                        foreach(GameObject go in gameObject.GetComponent<PathManager>().paths)
                        {
                            go.transform.GetChild(0).GetComponent<PathLogic>().UpdateNeighbours();
                        }
                    }

                }catch
                {
                    //clicked obj is not grid
                }
            }
        }
    }
}
