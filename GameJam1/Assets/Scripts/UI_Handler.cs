using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Handler : MonoBehaviour
{
    public bool isBuildingMode = false;
    [System.Serializable]
    public struct BuildingStruct{
        public GameObject modell; //the moddel of the given type
        public Vector3 cameraOffset; //The offset vector from the inforender camera
        public Vector3 instantiateOffset;//The vector used to offset the model when instantiating for gameplay and not info render
        public Vector3 instantiateScale;//The vector used to scale the model when instantiating for gameplay and not info render
        public Vector3 instantiateRotation;//The vector used for rotation when instantiating for gamplay
        public Vector3 presentationScale;
         public Vector3 presentationRotation;
        public bool presRotationOnX;
        public bool presRotationOnY;
        public bool presRotationOnZ;
        public structureType ownType;//The entry's own type
        public structureType canBeBuiltOn; //The grid-type that this type of building can be built on
        public string name; //the name of the building
        public string description; //the description of the building
    }

    public GameObject buildPanel;
    public BuildingStruct[] buildingStruct;
    public float distanceFromCamera;

    public Text nameText;
    public Text description;
    public Camera objectRenderCamera;

    public GameObject lightObject;

    public Image healthImage;
    public Text healthText;
    public Text[] resources; //wood,stone,humans
    public int populationCount = 200000;

    private GameManager gameManager;

    public BuildingStruct getCurrentlySelectedModell()
    {
        return buildingStruct[selectionIndex];
    }

    private int selectionIndex = 0;
    private GameObject currentlyInstantiated;
    private GameObject lo;

    private void Start() {
        gameManager = gameObject.GetComponent<GameManager>();
        UpdateHealthUI();
    }
    private void Update() {
        resources[0].text = gameManager.Wood.ToString();
        resources[1].text = gameManager.Stone.ToString();
        resources[2].text = gameManager.HumanResources.ToString();
        UpdateHealthUI();
    }

    public void UpdateInfo()
    {
        nameText.text = buildingStruct[selectionIndex].name;
        description.text = buildingStruct[selectionIndex].description;
        currentlyInstantiated = Instantiate(buildingStruct[selectionIndex].modell,objectRenderCamera.transform.position + buildingStruct[selectionIndex].cameraOffset,Quaternion.identity);
        if(buildingStruct[selectionIndex].presRotationOnX)
        {
            currentlyInstantiated.GetComponent<RotateObject>().x_axis_speed_multiplier = 1;
        }
        if(buildingStruct[selectionIndex].presRotationOnY)
        {
            currentlyInstantiated.GetComponent<RotateObject>().y_axis_speed_multiplier = 1;
        }
        if(buildingStruct[selectionIndex].presRotationOnZ)
        {
            currentlyInstantiated.GetComponent<RotateObject>().z_axis_speed_multiplier = 1;
        }
        currentlyInstantiated.transform.localScale = buildingStruct[selectionIndex].presentationScale;
        currentlyInstantiated.transform.rotation = Quaternion.Euler(buildingStruct[selectionIndex].presentationRotation);
        //currentlyInstantiated.AddComponent<Light>();
        if(lightObject != null && currentlyInstantiated.transform.childCount > 1)
        {
            lo = Instantiate(lightObject,objectRenderCamera.transform.position + new Vector3(0f,2f,0),Quaternion.identity);
            lo.transform.LookAt(currentlyInstantiated.transform);
            //lo.transform.parent = currentlyInstantiated.transform;
        }
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
        if(buildPanel.activeInHierarchy && !isBuildingMode)
        {
            buildPanel.SetActive(false);
            GameObject.Destroy(currentlyInstantiated);
            if(lo != null)
            {
                GameObject.Destroy(lo);
            }
        }else if(!buildPanel.activeInHierarchy)
        {
            UpdateInfo();
        }
    }
    public void NextBuilding()
    {
        if(!isBuildingMode)
        {
            GameObject.Destroy(currentlyInstantiated);
            StepSelectionIndex(true);
            if(lo != null)
            {
                GameObject.Destroy(lo);
            }
            UpdateInfo();
        }
    }
    public void PrevBuilding()
    {
        if(!isBuildingMode)
        {
            GameObject.Destroy(currentlyInstantiated);
            StepSelectionIndex(false);
            if(lo != null)
            {
                GameObject.Destroy(lo);
            }
            UpdateInfo();
        }
    }

    public void StartBuilding()
    {
        if(!isBuildingMode)
        {
            isBuildingMode = true;
            gameObject.GetComponent<GridClickHandler>().InstantiateGhost();
        }else
        {
            
        }
    }
    public void UpdateHealthUI()
    {
        healthImage.fillAmount = gameManager.CurrentHealth / gameManager.MaxHealth;
        healthText.text = "Population: " + Mathf.Floor(populationCount * healthImage.fillAmount).ToString();
    }
}
