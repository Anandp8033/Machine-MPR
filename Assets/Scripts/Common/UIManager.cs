using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    [Header("Home and Step Screen")]
    public GameObject HomeScreen;
    public GameObject stepScreen;
    public GameObject stepCompletedScreen;
    public TextMeshProUGUI step_num_text;
    public TextMeshProUGUI step_Heading_Text;
    public TextMeshProUGUI step_SubHeading_Text;
    public Button StartButton;
    public Button NextStepButton;
    [Header("Tools and Parts Screen")]
    public GameObject ToolsScreen;
    public GameObject PartsScreen;
    public GameObject ToolsPrefabParent;
    public GameObject PartsPrefabParent;
    public GameObject Prefab_items_Tools;
    public GameObject Prefab_items_Parts;

    public AudioManager audioManager;

    //public GameObject ToolsAndPartsScreen;
    //public GameObject partNToolsPrefabParent;
    //public GameObject ToolsAndPartsItemsPrefab;
    //public Button ToolsBtn;
    //public Button PartsBtn;


    [SerializeField]
    private GameObject Table;

    int activeChildCount = 0;


    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
        activeChildCount = 0;
    }

    public void Show_Home(bool isActive)
    {
        HomeScreen.SetActive(isActive);
        stepScreen.SetActive(false);
        stepCompletedScreen.SetActive(false);
    }
    public void Show_StepScreen(bool isActive) 
    {
        stepScreen.SetActive(isActive);
        HomeScreen.SetActive(false);
        stepCompletedScreen.SetActive(false);
    }

    public void Show_StepCompletedScreen(bool isActive,int currentStep)
    {
        stepCompletedScreen.SetActive(isActive);
        HomeScreen.SetActive(false);
        stepScreen.SetActive(false);
      //  ToolsAndPartsScreen.SetActive(false);
        ToolsScreen.SetActive(false);
        PartsScreen.SetActive(false);
        if (isActive && currentStep!=-1) 
        {
            audioManager.PlayEachStepCongratsAudio();
            NextStepButton.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Step {currentStep} completed Move to Next Step";
        }
    }

    //Instantiate Tools and parts items in the ToolsAndPartsScreen as per list provided from StepsController class PartsDetails
    //public void InstantiatePartsNTools(List<PartsDetails> partsDetails, string typeOfParts)
    //{
    //    //Clear previous items except the first child which does not have clone in its name
    //    foreach (Transform child in partNToolsPrefabParent.transform)
    //    {
    //        if(!child.name.Contains("(Clone)"))
    //            continue;
    //        Destroy(child.gameObject);
    //    }
    //    //Instantiate Tools and parts items in the ToolsAndPartsScreen as per list provided from StepsController class PartsDetails as per passed typeOfParts
    //    foreach (PartsDetails part in partsDetails)
    //    {
    //        if (part._partType == typeOfParts)
    //        {
    //            var partNToolsItem = Instantiate(ToolsAndPartsItemsPrefab, partNToolsPrefabParent.transform);
    //            partNToolsItem.SetActive(true);
    //            partNToolsItem.transform.GetChild(0).GetComponent<Image>().sprite = part.partImage;
    //            partNToolsItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = part.partID;
    //            var button = partNToolsItem.GetComponent<Button>();  
    //            button.onClick.AddListener(() =>
    //            {
    //                Debug.Log(part.partID + " clicked");
    //                //Clear previous gameobject under table if same part is already present then destroy it , only destroy same gameobject not other gameobject
    //                foreach (Transform child in Table.transform)
    //                {
    //                    if (child.transform.GetComponent<PartAttribute>())
    //                    {
    //                        Destroy(child.gameObject);
    //                    }
    //                }
    //                //Instantiate the part prefab at the center of the table with no rotation
    //                var gm =Instantiate(part.partPrefab, Table.transform);
    //                //gm.GetComponent<Rigidbody>().isKinematic = true; //only for test
    //                gm.transform.localPosition = new Vector3(0, 1f, 0.311f);
    //                if(part.part_Scale != Vector3.zero)
    //                {
    //                    gm.transform.localScale = part.part_Scale;
    //                }
    //                gm.transform.localRotation = Quaternion.identity;
    //            });
    //        }
    //    }

    //}

    //Instantiate Tools and parts items inside their saparate screens ToolsScreen and PartsScreen as per list provided from StepsController class PartsDetails
    public void Tools_Items_Instantiate(List<PartsDetails> partsDetails, string typeOfParts)
    {
        if(typeOfParts == "tools")
        {
            UiItemsOfTools_generate(partsDetails, typeOfParts);
        }
        else if(typeOfParts == "parts")
        {
            UiItemsOfParts_generate(partsDetails, typeOfParts);
        }
    }

    //UI items generator for tools 
    void UiItemsOfTools_generate(List<PartsDetails> partsDetails, string typeOfParts)
    {
        //Clear previous items except the first child which does not have clone in its name
        foreach (Transform child in ToolsPrefabParent.transform)
        {
            if (!child.name.Contains("(Clone)"))
            {
                child.gameObject.SetActive(false);
                continue;
            }
            Destroy(child.gameObject);
        }
        //Instantiate Tools and parts items in the ToolsAndPartsScreen as per list provided from StepsController class PartsDetails as per passed typeOfParts
        foreach (PartsDetails part in partsDetails)
        {
            if (part._partType == "tools")
            {
                var ToolsItem = Instantiate(Prefab_items_Tools, ToolsPrefabParent.transform);
                ToolsItem.SetActive(true);
                ToolsItem.transform.GetChild(0).GetComponent<Image>().sprite = part.partImage;
                ToolsItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = part.partID;
                var button = ToolsItem.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    Debug.Log(part.partID + " clicked");
                    //Clear previous gameobject under table if same part is already present then destroy it , only destroy same gameobject not other gameobject
                    foreach (Transform child in Table.transform)
                    {
                        if (child.transform.GetComponent<PartAttribute>())
                        {
                            Destroy(child.gameObject);
                        }
                    }
                    //Instantiate the part prefab at the center of the table with no rotation
                    var gm = Instantiate(part.partPrefab, Table.transform);
                    //gm.GetComponent<Rigidbody>().isKinematic = true; //only for test
                    gm.transform.localPosition = new Vector3(0, 1f, 0.311f);
                    if (part.part_Scale != Vector3.zero)
                    {
                        gm.transform.localScale = part.part_Scale;
                    }
                    gm.transform.localRotation = Quaternion.identity;
                });
                var tools_img = ToolsItem.transform.GetChild(0).GetComponent<Image>();
                // Clone material so each button has its own instance
                tools_img.material = new Material(tools_img.material);

                // Add hover script
                SpawnItemsFromUIList hoverScript = button.AddComponent<SpawnItemsFromUIList>();
                hoverScript.targetImage = tools_img;     
                hoverScript.TableTransform = Table;
                hoverScript.prefabToSpawn = part.partPrefab;
                hoverScript.spawnOffset = ToolsItem.transform;
                hoverScript.Parts_Size = part.part_Scale;
            }
        }         
    }

    //UI items generator for parts 
    void UiItemsOfParts_generate(List<PartsDetails> partsDetails, string typeOfParts)
    {
        //Clear previous items except the first child which does not have clone in its name
        foreach (Transform child in PartsPrefabParent.transform)
        {
            if (!child.name.Contains("(Clone)"))
            {
                child.gameObject.SetActive(false);
                continue;
            }
            Destroy(child.gameObject);
        }
        //Instantiate Tools and parts items in the ToolsAndPartsScreen as per list provided from StepsController class PartsDetails as per passed typeOfParts
        foreach (PartsDetails part in partsDetails)
        {
            if (part._partType == "parts")
            {
                var PartsItem = Instantiate(Prefab_items_Parts, PartsPrefabParent.transform);
                PartsItem.SetActive(true);
                PartsItem.transform.GetChild(0).GetComponent<Image>().sprite = part.partImage;
                PartsItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = part.partID;
                var button = PartsItem.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    Debug.Log(part.partID + " clicked");
                    //Clear previous gameobject under table if same part is already present then destroy it , only destroy same gameobject not other gameobject
                    foreach (Transform child in Table.transform)
                    {
                        if (child.transform.GetComponent<PartAttribute>())
                        {
                            Destroy(child.gameObject);
                        }
                    }
                    //Instantiate the part prefab at the center of the table with no rotation
                    var gm = Instantiate(part.partPrefab, Table.transform);
                    //gm.GetComponent<Rigidbody>().isKinematic = true; //only for test
                    gm.transform.localPosition = new Vector3(0, 1f, 0.311f);
                    if (part.part_Scale != Vector3.zero)
                    {
                        gm.transform.localScale = part.part_Scale;
                    }
                    gm.transform.localRotation = Quaternion.identity;
                });
                var tools_img = PartsItem.transform.GetChild(0).GetComponent<Image>();
                // Clone material so each button has its own instance
                tools_img.material = new Material(tools_img.material);

                // Add hover script
                SpawnItemsFromUIList hoverScript = button.AddComponent<SpawnItemsFromUIList>();
                hoverScript.targetImage = tools_img;
                hoverScript.TableTransform = Table;
                hoverScript.prefabToSpawn = part.partPrefab;
                hoverScript.spawnOffset = PartsItem.transform;
                hoverScript.Parts_Size = part.part_Scale;
            }
        }
    }



    //enable disable content size fitter on partToolsPrefabParent if under that have more that 8 active children
    //public void EnableDisableContentSizeFitter()
    //{
    //    var contentSizeFitter = partNToolsPrefabParent.GetComponent<ContentSizeFitter>();
        
    //    foreach (Transform child in partNToolsPrefabParent.transform)
    //    {
    //        if (child.gameObject.activeSelf)
    //        {
    //            activeChildCount++;
    //        }
    //    }
    //    if (activeChildCount > 8)
    //    {
    //        contentSizeFitter.enabled = true;
    //        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
    //    }
    //    else
    //    {
    //        contentSizeFitter.enabled = false;
    //        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
    //    }
    //}

    public void Show_ToolsAndPartsScreen(bool isActive)
    {
        ToolsScreen.SetActive(isActive);
        PartsScreen.SetActive(isActive);
        //ToolsAndPartsScreen.SetActive(isActive);        
    }

    //on all steps completed show Show_StepCompletedScreen and change text of NextStepButton to Finish and text title
    public void Show_AllStepsCompletedScreen()
    {
        Show_StepCompletedScreen(true,-1);
        audioManager.PlayFinalStepCongratsAudio();
        NextStepButton.GetComponentInChildren<TextMeshProUGUI>().text = "Finish";
        NextStepButton.onClick.RemoveAllListeners();
        NextStepButton.onClick.AddListener(() => Application.Quit());
        NextStepButton.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "All Steps Completed";
    }
}
