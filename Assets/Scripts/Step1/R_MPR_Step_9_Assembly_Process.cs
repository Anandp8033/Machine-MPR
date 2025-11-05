using DG.Tweening;
using System;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;


public class R_MPR_Step_9_Assembly_Process : StepAssemblyProcessBase
{
    public GameObject _DEV02704_SLIDE_HOUSE;
    public GameObject _204121_SEAT;
    public GameObject _204121_SEAT_Highlighted;
    public GameObject _238398_SPRING;
    public GameObject _238398_SPRING_Highlighted;
    public GameObject _2471383_COVER;
    public GameObject _2471383_COVER_Highlighted;
    public GameObject _512024_SOCKET_SCREW_DIN912_1;
    public GameObject _512024_SOCKET_SCREW_DIN912_2;
    public GameObject _512024_SOCKET_SCREW_DIN912_3;
    public GameObject _512024_SOCKET_SCREW_DIN912_1_Highlighted;
    public GameObject _512024_SOCKET_SCREW_DIN912_2_Highlighted;
    public GameObject _512024_SOCKET_SCREW_DIN912_3_Highlighted;
    public GameObject toltipCanvas;
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
        _UIManager.step_num_text.text = "<size=10>STEP:9</size>";
        _UIManager.step_Heading_Text.text = "INSERT SEAT AND COVER WITH SPRING IN BETWEEN ON TO SLIDE HOUSE.";
        _UIManager.step_SubHeading_Text.text = "Click and generate DEV02704 SLIDE HOUSE from the panel, grab it from table and move towards highlighted part.";
        step_1();
    }


    //disable all the assembly parts 
    public void DisableAllParts()
    {
        _DEV02704_SLIDE_HOUSE.SetActive(false);
        _204121_SEAT.SetActive(false);
        _204121_SEAT_Highlighted.SetActive(false);
        _238398_SPRING.SetActive(false);
        _238398_SPRING_Highlighted.SetActive(false);
        _2471383_COVER.SetActive(false);
        _2471383_COVER_Highlighted.SetActive(false);
        _512024_SOCKET_SCREW_DIN912_1.SetActive(false);
        _512024_SOCKET_SCREW_DIN912_2.SetActive(false);
        _512024_SOCKET_SCREW_DIN912_3.SetActive(false);
        _512024_SOCKET_SCREW_DIN912_1_Highlighted.SetActive(false);
        _512024_SOCKET_SCREW_DIN912_2_Highlighted.SetActive(false);
        _512024_SOCKET_SCREW_DIN912_3_Highlighted.SetActive(false);
        toltipCanvas.SetActive(false);
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
                sequence2.Append(_238398_SPRING_Highlighted.transform.DOMove(_238398_SPRING.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence2.onComplete = () =>
                {
                    sequence2.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 3:
                Sequence sequence3 = DOTween.Sequence();
                sequence3.Append(_2471383_COVER_Highlighted.transform.DOMove(_2471383_COVER.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence3.onComplete = () =>
                {
                    sequence3.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 4:
                Sequence sequence4 = DOTween.Sequence();
                sequence4.Append(_512024_SOCKET_SCREW_DIN912_1_Highlighted.transform.DOMove(_512024_SOCKET_SCREW_DIN912_1.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence4.onComplete = () =>
                {
                    sequence4.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 5:
                Sequence sequence5 = DOTween.Sequence();
                sequence5.Append(_512024_SOCKET_SCREW_DIN912_2_Highlighted.transform.DOMove(_512024_SOCKET_SCREW_DIN912_2.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence5.onComplete = () =>
                {
                    sequence5.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 6:
                Sequence sequence6 = DOTween.Sequence();
                sequence6.Append(_512024_SOCKET_SCREW_DIN912_3_Highlighted.transform.DOMove(_512024_SOCKET_SCREW_DIN912_3.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence6.onComplete = () =>
                {
                    sequence6.Kill();
                    toltipCanvas.SetActive(false);
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
                _UIManager.step_SubHeading_Text.text = "Click and generate 238398 SPRING from the panel, grab it from table and move towards highlighted part.";
                step_3();
                break;
            case 3:
                _UIManager.step_SubHeading_Text.text = "Click and generate 2471383 COVER from the panel, grab it from table and move towards highlighted part.";
                step_4();
                break;
            case 4:
                _UIManager.step_SubHeading_Text.text = "Click and generate 512024 SOCKET SCREW  from the panel, grab it from table and move towards highlighted part.";
                step_5();
                break;
            case 5:
                _UIManager.step_SubHeading_Text.text = "Click and generate 512024 SOCKET SCREW  from the panel, grab it from table and move towards highlighted part.";
                step_6();
                break;
            case 6:
                _UIManager.step_SubHeading_Text.text = "Click and generate 512024 SOCKET SCREW  from the panel, grab it from table and move towards highlighted part.";
                step_7();
                break;  
            case 7:
                _UIManager.step_SubHeading_Text.text = "Step 9 assembly Process completed.";
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
        _238398_SPRING_Highlighted.SetActive(true);
        snapAbleHandler = _238398_SPRING_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_4()
    {
        _2471383_COVER_Highlighted.SetActive(true);
        snapAbleHandler = _2471383_COVER_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_5()
    {
        toltipCanvas.SetActive(true);
        _512024_SOCKET_SCREW_DIN912_1_Highlighted.SetActive(true); 
        snapAbleHandler = _512024_SOCKET_SCREW_DIN912_1_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
        toltipCanvas.transform.localPosition = new Vector3(0.111f, 0.264f, -1.229f);
    }

    public void step_6()
    {
        _512024_SOCKET_SCREW_DIN912_2_Highlighted.SetActive(true);
        snapAbleHandler = _512024_SOCKET_SCREW_DIN912_2_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
        toltipCanvas.transform.localPosition = new Vector3(0.111f, 0f, -1.229f);
    }

    public void step_7()
    {
        _512024_SOCKET_SCREW_DIN912_3_Highlighted.SetActive(true);
        snapAbleHandler = _512024_SOCKET_SCREW_DIN912_3_Highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
        toltipCanvas.transform.localPosition = new Vector3(0.462f, 0.161f, -1.154f);
    }
}
