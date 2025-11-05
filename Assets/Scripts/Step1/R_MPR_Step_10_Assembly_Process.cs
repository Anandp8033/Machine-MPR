using DG.Tweening;
using System;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;


public class R_MPR_Step_10_Assembly_Process : StepAssemblyProcessBase
{
    public GameObject _DEV02704_SLIDE_HOUSE;
    public GameObject _204121_SEAT;
    public GameObject _204121_SEAT_Highlighted;
    public GameObject _223052_SPACER;
    public GameObject _223052_SPACER_Highlighted;  
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
        _UIManager.step_num_text .text = "<size=10>STEP:10</size>";
        _UIManager.step_Heading_Text .text = "INSERT PREASSEMBLED SEAT AND SPACER ON TO SLIDE HOUSE.";
        _UIManager.step_SubHeading_Text.text = "Click and generate DEV02704 SLIDE HOUSE from the panel, grab it from table and move towards highlighted part.";
        step_1();
    }


    //disable all the assembly parts 
    public void DisableAllParts()
    {
        _DEV02704_SLIDE_HOUSE.SetActive(false);
        _204121_SEAT.SetActive(false);
        _204121_SEAT_Highlighted.SetActive(false);
       _223052_SPACER.SetActive(false);
        _223052_SPACER_Highlighted.SetActive(false);
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
                sequence1.Append(_204121_SEAT_Highlighted.transform.DOMove(_204121_SEAT.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence1.onComplete = () =>
                {
                    sequence1.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 2:
                Sequence sequence2 = DOTween.Sequence();
                sequence2.Append(_223052_SPACER_Highlighted.transform.DOMove(_223052_SPACER.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence2.onComplete = () =>
                {
                    sequence2.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 3:
                Sequence sequence3 = DOTween.Sequence();
               // sequence3.Append(_2471383_COVER_Highlighted.transform.DOMove(_2471383_COVER.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence3.onComplete = () =>
                {
                    sequence3.Kill();
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
                _UIManager.step_SubHeading_Text.text = "Click and generate 204121 SEAT from the panel, grab it from table and move towards highlighted part.";
                step_2();
                break;
            case 2:
                _UIManager.step_SubHeading_Text.text = "Click and generate 223052_SPACER from the panel, grab it from table and move towards highlighted part.";
                step_3();
                break;            
            case 3:
                _UIManager.step_SubHeading_Text.text = "Step 10 assembly Process completed.";
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
        _204121_SEAT_Highlighted.SetActive(true);
        snapAbleHandler = _204121_SEAT_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_3()
    {
        _223052_SPACER_Highlighted.SetActive(true);
        snapAbleHandler = _223052_SPACER_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }
}
