using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    [System.Serializable]
    public struct BuildingStruct{
        public GameObject modell;
        public Vector3 cameraOffset;

        public structureType canBeBuiltOn;
        public string name;
        public string description;
    }

    public GameObject buildPanel;
    public BuildingStruct[] buildingStruct;
    public float distanceFromCamera;

    public Text nameText;
    public Text description;
    public Camera objectRenderCamera;

    private int selectionIndex = 0;
    private GameObject currentlyInstantiated;

    public void UpdateInfo()
    {
        nameText.text = buildingStruct[selectionIndex].name;
        description.text = buildingStruct[selectionIndex].description;
        currentlyInstantiated = Instantiate(buildingStruct[selectionIndex].modell,objectRenderCamera.transform.position + buildingStruct[selectionIndex].cameraOffset,Quaternion.identity);
        currentlyInstantiated.GetComponent<RotateObject>().y_axis_speed_multiplier = 1;
        currentlyInstantiated.transform.localScale = new Vector3(1,1,1);
        buildPanel.SetActive(true);
    }

    public void StepSelectionIndex(bool isIncrement)
    {
        if(isIncrement)
        {
            selectionIndex++;
            if(selectionIndex == buildingStruct.Length)
            {
                selectionIndex = 0;
            }
        }else
        {
            selectionIndex--;
            if(selectionIndex == -1)
            {
                selectionIndex = buildingStruct.Length-1;
            }
        }
    }
    public void onBuildButtonClicked()
    {
        if(buildPanel.activeInHierarchy)
        {
            buildPanel.SetActive(false);
            GameObject.Destroy(currentlyInstantiated);
        }else
        {
            UpdateInfo();
        }
    }
    public void NextBuilding()
    {
        GameObject.Destroy(currentlyInstantiated);
        StepSelectionIndex(true);
        UpdateInfo();
    }
    public void PrevBuilding()
    {
        GameObject.Destroy(currentlyInstantiated);
        StepSelectionIndex(false);
        UpdateInfo();
    }

    public void StartBuilding()
    {
        
    }
}
