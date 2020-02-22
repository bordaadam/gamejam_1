using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClickHandler : MonoBehaviour
{
    public GameObject path;
    public GameObject Wood_Gatherer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit  hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                  
            if (Physics.Raycast(ray, out hit))
            {
                //Wood_Gatherer placement
                try //We expect to have a grid component attached to the hit object
                {
                    if(hit.transform.gameObject.GetComponent<GameGrid>().structure == structureType.WOOD)
                    {
                        //TODO: Build mode and building type check
                        hit.transform.gameObject.GetComponent<GameGrid>().structure = structureType.WOOD_GATHERER;
                        GameObject tmp = Instantiate(Wood_Gatherer,new Vector3(hit.transform.position.x,hit.transform.position.y + 0.51f,hit.transform.position.z),Quaternion.identity);
                        hit.transform.gameObject.GetComponent<GameGrid>().objectsHeld[1] = tmp;
                        //hit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.red);
                    }

                    //Path placement
                    if(hit.transform.gameObject.GetComponent<GameGrid>().structure == structureType.NOTHING)
                    {
                        //TODO: Build mode and building type check
                        hit.transform.gameObject.GetComponent<GameGrid>().structure = structureType.PATH;
                        GameObject tmp = Instantiate(path,new Vector3(hit.transform.position.x,hit.transform.position.y + 0.51f,hit.transform.position.z),Quaternion.identity);
                        Vector3 temp = transform.rotation.eulerAngles;
                        temp.x = 90.0f;
                        tmp.transform.rotation = Quaternion.Euler(temp);
                        hit.transform.gameObject.GetComponent<GameGrid>().objectsHeld[1] = tmp;
                        //hit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.yellow);
                    }

                }catch
                {
                    //clicked obj is not grid
                }
            }
        }
    }
}
