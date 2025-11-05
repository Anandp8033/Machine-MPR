using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PartsDetails
{
    public GameObject partPrefab;
    public Sprite partImage;
    public Vector3 part_Scale = Vector3.zero;
    public string partID;
    public string _partType;    
    public AudioClip instructionClip;
    public string instructionText;
}

[CreateAssetMenu(fileName = "StepsController", menuName = "RotatorScriptableObjects/StepsController")]
public class StepsController : ScriptableObject
{
    public List<GameObject> _parts;
    public SpawnPointData _parts_Transform;
    public List<PartsDetails> partsDetails;   

    //public List<GameObject> assemblypartsObjects;
    public GameObject asseebleedPart;
    public GameObject assemblyPart;

    public event Action OnStepComplete;

    private UIManager _uiManager;

    public void Intialize(GameObject Table,UIManager uiManager)
    {
        _uiManager = uiManager;
        _uiManager?.Show_Home(false);
        _uiManager?.Show_ToolsAndPartsScreen(false);
        //_uiManager?.PartsBtn.onClick.AddListener(() => _uiManager?.InstantiatePartsNTools(partsDetails, "parts"));
        //_uiManager?.ToolsBtn.onClick.AddListener(() => _uiManager?.InstantiatePartsNTools(partsDetails, "tools"));
        _uiManager?.Tools_Items_Instantiate(partsDetails, "tools");
        _uiManager?.Tools_Items_Instantiate(partsDetails, "parts");
        var assembledPartInstance = Instantiate(asseebleedPart, Table.transform);
        assembledPartInstance.GetComponent<AssembledPartAnimationBase>().StartExplodeAnimation(() => { AnimationEnd(assembledPartInstance, Table); });       

    }

    //assign all child of assembly part to assemblypartsObjects list each assembly part have 

    public void AnimationEnd(GameObject assembledPartInstance,GameObject Table)
    {
        _uiManager?.Show_StepScreen(true);
        assembledPartInstance.SetActive(false);
        _uiManager?.Show_ToolsAndPartsScreen(true);
        //_uiManager?.InstantiatePartsNTools(partsDetails, "parts");

        //if(_parts.Count != _parts_Transform.localTransform.Count)
        //{
        //    Debug.LogError("Parts count and Transform count are not equal");
        //    return;
        //}
        //for(int i = 0; i < _parts.Count; i++)
        //{
        //    var partInstance = Instantiate(_parts[i] , Table.transform);
        //    partInstance.transform.localPosition = _parts_Transform.localTransform[i].localPosition;
        //    partInstance.transform.localEulerAngles = _parts_Transform.localTransform[i].localRotation;
        //    partInstance.transform.GetComponent<Rigidbody>().isKinematic = false;
        //}
        var _assemblyPartInstance = Instantiate(assemblyPart, Table.transform);
        _assemblyPartInstance.GetComponent<StepAssemblyProcessBase>().Initialize(_uiManager);
        _assemblyPartInstance.GetComponent<StepAssemblyProcessBase>().OnStepComplete += () => 
        {
            Debug.Log("Step Completed");
            OnStepComplete?.Invoke();
        };
    }

}
