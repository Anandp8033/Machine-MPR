//using UnityEngine;
//using UnityEngine.UI;

//public class Temp_UI_Swap : MonoBehaviour
//{
//    [Header("Tools and Parts Screen_Vertical")]
//    public GameObject ToolsAndPartsScreen;
//    public GameObject partNToolsPrefabParent;
//    public GameObject ToolsAndPartsItemsPrefab;
//    public Button ToolsBtn;
//    public Button PartsBtn;

//    [Header("Tools and Parts Screen_Horizontal")]
//    public GameObject H_ToolsAndPartsScreen;
//    public GameObject H_partNToolsPrefabParent;
//    public GameObject H_ToolsAndPartsItemsPrefab;
//    public Button H_ToolsBtn;
//    public Button H_PartsBtn;

//    public UIManager uIManager;
//    public bool isHorizontal = false;



//    public void switchRefrenceofUIManagerToHorizontal()
//    {
//        isHorizontal =! isHorizontal;
//        if (isHorizontal) {
//            uIManager.partNToolsPrefabParent = H_partNToolsPrefabParent;
//            uIManager.ToolsAndPartsItemsPrefab = H_ToolsAndPartsItemsPrefab;
//            uIManager.ToolsBtn = H_ToolsBtn;
//            uIManager.PartsBtn = H_PartsBtn;
//            ToolsAndPartsScreen.SetActive(false);
//            H_ToolsAndPartsScreen.SetActive(true);
//        }
//        else
//        {
//            uIManager.partNToolsPrefabParent = partNToolsPrefabParent;
//            uIManager.ToolsAndPartsItemsPrefab = ToolsAndPartsItemsPrefab;
//            uIManager.ToolsBtn = ToolsBtn;
//            uIManager.PartsBtn = PartsBtn;
//            H_ToolsAndPartsScreen.SetActive(false);
//            ToolsAndPartsScreen.SetActive(true);
//        }
//    }

//}
