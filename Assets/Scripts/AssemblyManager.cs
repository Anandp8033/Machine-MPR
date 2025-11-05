using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyManager : MonoBehaviour
{
    public GameObject Table;
    public List<StepsController> _stepsControllers;
    public int currentStep = 0;
    public UIManager UIManager;

    void Start()
    {
        UIManager.Show_Home(true);
        UIManager.Show_ToolsAndPartsScreen(false);
        UIManager.StartButton.GetComponent<Button>().onClick.AddListener(() => startAssemblyProcess());
        UIManager.NextStepButton.GetComponent<Button>().onClick.AddListener(() => startAssemblyProcess());


    }

    void startAssemblyProcess()
    {
        UIManager.audioManager.StopAllAudio();
        CleanPreviousStepData();
        _stepsControllers[currentStep].Intialize(Table, UIManager);
        _stepsControllers[currentStep].OnStepComplete += CurrentStepCompleted;
    }

    public void StartSpecifiedStep(int stepNum)
    {
        CleanPreviousStepData();
        currentStep = stepNum;
        _stepsControllers[currentStep].Intialize(Table, UIManager);
        _stepsControllers[currentStep].OnStepComplete += CurrentStepCompleted;
    }

    private void CurrentStepCompleted()
    {
        _stepsControllers[currentStep].OnStepComplete -= CurrentStepCompleted;
        currentStep++;
        if (currentStep < _stepsControllers.Count)
        {
            UIManager.Show_StepCompletedScreen(true, currentStep);
        }
        else
        {
            Debug.Log("Assembly Process Completed");
            UIManager.Show_AllStepsCompletedScreen();
        }
    }

    //clean all previous step data when new step start
    private void CleanPreviousStepData()
    {
        if (Table.transform.childCount == 0) return;
        foreach (Transform child in Table.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
