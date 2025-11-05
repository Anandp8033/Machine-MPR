using DG.Tweening;
using System;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;


public class R_MPR_Step_12_Assembly_Process : StepAssemblyProcessBase
{
    public GameObject _DEV02704_SLIDE_HOUSE;
    public GameObject _420514_O_Ring_1;
    public GameObject _420514_O_Ring_1_Highlighted;
    public GameObject _420514_O_Ring_2;
    public GameObject _420514_O_Ring_2_Highlighted;
    public GameObject _420514_O_Ring_3;
    public GameObject _420514_O_Ring_3_Highlighted;
    public GameObject _420514_O_Ring_4;
    public GameObject _420514_O_Ring_4_Highlighted;
    public GameObject _420514_O_Ring_5;
    public GameObject _420514_O_Ring_5_Highlighted;

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
        _UIManager.step_num_text.text = "<size=10>STEP:12</size>";
        _UIManager.step_Heading_Text.text = "ASSEMBLE O-RINGS ON INTERFACE SIDE";
        _UIManager.step_SubHeading_Text.text = "Click and generate DEV02704 SLIDE_HOUSE from the panel, grab it from table and move towards highlighted part.";
        step_1();
    }


    //disable all the assembly parts 
    public void DisableAllParts()
    {
        _DEV02704_SLIDE_HOUSE.SetActive(false);
        _420514_O_Ring_1.SetActive(false);
        _420514_O_Ring_1_Highlighted.SetActive(false);
        _420514_O_Ring_2.SetActive(false);
        _420514_O_Ring_2_Highlighted.SetActive(false);
        _420514_O_Ring_3.SetActive(false);
        _420514_O_Ring_3_Highlighted.SetActive(false);
        _420514_O_Ring_4.SetActive(false);
        _420514_O_Ring_4_Highlighted.SetActive(false);
        _420514_O_Ring_5.SetActive(false);
        _420514_O_Ring_5_Highlighted.SetActive(false);
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
                sequence1.Append(_420514_O_Ring_1_Highlighted.transform.DOMove(_420514_O_Ring_1.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence1.onComplete = () =>
                {
                    sequence1.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 2:
                Sequence sequence2 = DOTween.Sequence();
                sequence2.Append(_420514_O_Ring_2_Highlighted.transform.DOMove(_420514_O_Ring_2.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence2.onComplete = () =>
                {
                    sequence2.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 3:
                Sequence sequence3 = DOTween.Sequence();
                sequence3.Append(_420514_O_Ring_3_Highlighted.transform.DOMove(_420514_O_Ring_3.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence3.onComplete = () =>
                {
                    sequence3.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 4:
                Sequence sequence4 = DOTween.Sequence();
                sequence4.Append(_420514_O_Ring_4_Highlighted.transform.DOMove(_420514_O_Ring_4.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence4.onComplete = () =>
                {
                    sequence4.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 5:
                Sequence sequence5 = DOTween.Sequence();
                sequence5.Append(_420514_O_Ring_5_Highlighted.transform.DOMove(_420514_O_Ring_5.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence5.onComplete = () =>
                {
                    sequence5.Kill();
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
                _UIManager.step_SubHeading_Text.text = "Click and generate 420514_O_Ring from the panel, grab it from table and move towards highlighted part.";
                step_2();
                break;
            case 2:
                _UIManager.step_SubHeading_Text.text = "Click and generate 420514_O_Ring from the panel, grab it from table and move towards highlighted part.";
                step_3();
                break;
            case 3:
                _UIManager.step_SubHeading_Text.text = "Click and generate 420514_O_Ring from the panel, grab it from table and move towards highlighted part.";
                step_4();
                break;
            case 4:
                _UIManager.step_SubHeading_Text.text = "Click and generate 420514_O_Ring from the panel, grab it from table and move towards highlighted part.";
                step_5();
                break;
            case 5:
                _UIManager.step_SubHeading_Text.text = "Click and generate 420514_O_Ring from the panel, grab it from table and move towards highlighted part.";
                step_6();
                break;            
            case 6:
                _UIManager.step_SubHeading_Text.text = "Step 12 assembly Process completed.";
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
        _420514_O_Ring_1_Highlighted.SetActive(true);
        snapAbleHandler = _420514_O_Ring_1_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_3()
    {
        _420514_O_Ring_2_Highlighted.SetActive(true);
        snapAbleHandler = _420514_O_Ring_2_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_4()
    {
        _420514_O_Ring_3_Highlighted.SetActive(true);
        snapAbleHandler = _420514_O_Ring_3_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_5()
    {
        _420514_O_Ring_4_Highlighted.SetActive(true);
        snapAbleHandler = _420514_O_Ring_4_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }
    public void step_6()
    {
        _420514_O_Ring_5_Highlighted.SetActive(true);
        snapAbleHandler = _420514_O_Ring_5_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }
}
