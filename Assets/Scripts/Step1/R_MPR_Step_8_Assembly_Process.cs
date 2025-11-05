using DG.Tweening;
using System;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;


public class R_MPR_Step_8_Assembly_Process : StepAssemblyProcessBase
{
    public GameObject _DEV02704_SLIDE_HOUSE;
    public GameObject _2031179_PISTON;
    public GameObject _2031179_PISTON_highlighted;
    public int currentStep = 0;
    SnapAblePartHandler snapAbleHandler;
    private UIManager _UIManager;

    public event Action Onstep_2Complete;

    private void Awake()
    {
    }

    void Start()
    {
        currentStep = 0;
    }

    // Intitiaze the assembly parts to perform the assembly
    public override void Initialize(UIManager _uiMananger)
    {
        _UIManager = _uiMananger;
        DisableAllParts();
        Debug.Log("init step 1 ass");
        _UIManager.step_num_text.text = "<size=10>STEP:8</size>";
        _UIManager.step_Heading_Text.text = "INSERT PISTON IN TO HOUSE.";
        _UIManager.step_SubHeading_Text.text = "Click and generate 2031179 PISTON from the panel, grab it from table and move towards highlighted part.";
        step_1();
    }


    //disable all the assembly parts 
    public void DisableAllParts()
    {
        _DEV02704_SLIDE_HOUSE.SetActive(false);
        _2031179_PISTON.SetActive(false);
        _2031179_PISTON_highlighted.SetActive(false);
    }


    // Event handler for when a part is snapped
    private void Handler_OnSnapped(SnapAblePartHandler sender)
    {
        Debug.Log("Part snapped: " + sender.snapTagId);
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        MoveTowardsplacedGm();
        //currentStep++;
        //StepSwitch();
    }

    private void MoveTowardsplacedGm()
    {
        switch (currentStep)
        {
            case 0:
                currentStep++;
                StepSwitch();
                break;
            case 1:
                Sequence sequence1 = DOTween.Sequence();
                sequence1.Append(_2031179_PISTON_highlighted.transform.DOMove(_2031179_PISTON.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence1.onComplete = () =>
                {
                    sequence1.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;                      
        }
    }

    //calling next step
    public void StepSwitch()
    {
        Debug.Log("Current Step: " + currentStep);
        switch (currentStep)
        {
            case 1:
                _UIManager.step_SubHeading_Text.text = "Click and generate 2031179 PISTON from the panel, grab it from table and move towards highlighted part.";
                step_2();
                break;
            case 2:
                _UIManager.step_SubHeading_Text.text = "Step 8 assembly Process completed.";
                StepCompleted();
                break;
        }
    }
    // Call this when the step is completed
    private void StepCompleted()
    {
        Debug.Log("Step_4 Completed");
        InvokeStepComplete();
    }


    public void step_1()
    {
        _DEV02704_SLIDE_HOUSE.SetActive(true);
        snapAbleHandler = _DEV02704_SLIDE_HOUSE.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }


    public void step_2()
    {
     _2031179_PISTON_highlighted.SetActive(true);
      snapAbleHandler = _2031179_PISTON_highlighted.GetComponent<SnapAblePartHandler>();
      snapAbleHandler.OnSnapped += Handler_OnSnapped;
      snapAbleHandler.Initialize();
    }
}
