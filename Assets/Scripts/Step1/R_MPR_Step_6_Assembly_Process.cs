using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;


public class R_MPR_Step_6_Assembly_Process : StepAssemblyProcessBase
{
    public GameObject _DEV02704_SLIDE_HOUSE;
    public List<GameObject> _575018_HELI_COIL_Side_1;
    public List<GameObject> _575018_HELI_COIL_Side_1_higlighted;
    public List<GameObject> _575018_HELI_COIL_Side_2;
    public List<GameObject> _575018_HELI_COIL_Side_2_higlighted;
    public GameObject _216150_PLUG;
    public GameObject _216150_PLUG_highlighted;
    public int currentStep = 0;
    public Animator _animator;
    SnapAblePartHandler snapAbleHandler;
    private UIManager _UIManager;

    public event Action Onstep_2Complete;

    private void Awake()
    {
    }

    void Start()
    {
        currentStep = 0;
        _animator = GetComponent<Animator>();
    }

    // Intitiaze the assembly parts to perform the assembly
    public override void Initialize(UIManager _uiMananger)
    {
        _UIManager = _uiMananger;
        DisableAllParts();
        Debug.Log("init step 1 ass");
        _UIManager.step_num_text.text = "<size=10>STEP:6</size>";
        _UIManager.step_Heading_Text.text = "ASSEMBLE HELI-COIL AND PLUG ON TO SLIDE HOUSE.";
        _UIManager.step_SubHeading_Text.text = "Click and generate DEV02704 SLIDE HOUSE from the panel, grab it from table and move towards highlighted part.";
        step_1();
    }


    //disable all the assembly parts 
    public void DisableAllParts()
    {
        _DEV02704_SLIDE_HOUSE.SetActive(false);
        foreach(var item in _575018_HELI_COIL_Side_1) item.SetActive(false);
        foreach(var item in _575018_HELI_COIL_Side_1_higlighted) item.SetActive(false);
        foreach(var item in _575018_HELI_COIL_Side_2) item.SetActive(false);
        foreach(var item in _575018_HELI_COIL_Side_2_higlighted) item.SetActive(false);
        _216150_PLUG.SetActive(false);
        _216150_PLUG_highlighted.SetActive(false);
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
                sequence1.Append(_575018_HELI_COIL_Side_1_higlighted[0].transform.DOMove(_575018_HELI_COIL_Side_1[0].transform.position, 1f).SetEase(Ease.InOutSine));
                sequence1.onComplete = () =>
                {
                    sequence1.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 2:
                Sequence sequence2 = DOTween.Sequence();
                sequence2.Append(_575018_HELI_COIL_Side_1_higlighted[1].transform.DOMove(_575018_HELI_COIL_Side_1[1].transform.position, 1f).SetEase(Ease.InOutSine));
                sequence2.onComplete = () =>
                {
                    sequence2.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 3:
                Sequence sequence3 = DOTween.Sequence();
                sequence3.Append(_575018_HELI_COIL_Side_1_higlighted[2].transform.DOMove(_575018_HELI_COIL_Side_1[2].transform.position, 1f).SetEase(Ease.InOutSine));
                sequence3.onComplete = () =>
                {
                    sequence3.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 4:
                Sequence sequence4 = DOTween.Sequence();
                sequence4.Append(_575018_HELI_COIL_Side_2_higlighted[0].transform.DOMove(_575018_HELI_COIL_Side_2[0].transform.position, 1f).SetEase(Ease.InOutSine));
                sequence4.onComplete = () =>
                {
                    sequence4.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 5:
                Sequence sequence5 = DOTween.Sequence();
                sequence5.Append(_575018_HELI_COIL_Side_2_higlighted[1].transform.DOMove(_575018_HELI_COIL_Side_2[1].transform.position, 1f).SetEase(Ease.InOutSine));
                sequence5.onComplete = () =>
                {
                    sequence5.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 6:
                Sequence sequence6 = DOTween.Sequence();
                sequence6.Append(_575018_HELI_COIL_Side_2_higlighted[2].transform.DOMove(_575018_HELI_COIL_Side_2[2].transform.position, 1f).SetEase(Ease.InOutSine));
                sequence6.onComplete = () =>
                {
                    sequence6.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 7:
                Sequence sequence7 = DOTween.Sequence();
                sequence7.Append(_575018_HELI_COIL_Side_2_higlighted[3].transform.DOMove(_575018_HELI_COIL_Side_2[3].transform.position, 1f).SetEase(Ease.InOutSine));
                sequence7.onComplete = () =>
                {
                    sequence7.Kill();
                    currentStep++;
                    StepSwitch();
                };
                break;
            case 8:
                Sequence sequence8 = DOTween.Sequence();
                sequence8.Append(_216150_PLUG_highlighted.transform.DOMove(_216150_PLUG.transform.position, 1f).SetEase(Ease.InOutSine));
                sequence8.onComplete = () =>
                {
                    sequence8.Kill();
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
                _UIManager.step_SubHeading_Text.text = "Click and generate 575018 HELI COIL from the panel, grab it from table and move towards highlighted part.";
                step_2();
                break;
            case 2:
                _UIManager.step_SubHeading_Text.text = "Click and generate 575018 HELI COIL from the panel, grab it from table and move towards highlighted part.";
                step_3();
                break;
            case 3:
                _UIManager.step_SubHeading_Text.text = "Click and generate 575018 HELI COIL from the panel, grab it from table and move towards highlighted part.";
                step_4();
                break;
            case 4:
                _UIManager.step_SubHeading_Text.text = "Click and generate 575018 HELI COIL from the panel, grab it from table and move towards highlighted part.";
                step_5();
                break;
            case 5:
                _UIManager.step_SubHeading_Text.text = "Click and generate 575018 HELI COIL from the panel, grab it from table and move towards highlighted part.";
                step_6();
                break;
            case 6:
                _UIManager.step_SubHeading_Text.text = "Click and generate 575018 HELI COIL from the panel, grab it from table and move towards highlighted part.";
                step_7();
                break;
            case 7:
                _UIManager.step_SubHeading_Text.text = "Click and generate 575018 HELI COIL from the panel, grab it from table and move towards highlighted part.";
                step_8();
                break;
            case 8:
                _UIManager.step_SubHeading_Text.text = "Click and generate 216150 PLUG from the panel, grab it from table and move towards highlighted part.";
                step_9();
                break;
            case 9:
                Debug.Log("Assembly Process Completed!");
                _UIManager.step_SubHeading_Text.text = "Step 6 assembly Process completed.";
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
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_DEV02704_SLIDE_HOUSE.transform.DORotate(new Vector3(0, -30, 0), 1f).SetEase(Ease.InOutSine));
        sequence.onComplete = () =>
        {
            sequence.Kill();
            _575018_HELI_COIL_Side_1_higlighted[0].SetActive(true);
            snapAbleHandler = _575018_HELI_COIL_Side_1_higlighted[0].GetComponent<SnapAblePartHandler>();
            snapAbleHandler.OnSnapped += Handler_OnSnapped;
            snapAbleHandler.Initialize();
        };
    }

    public void step_3()
    {
        _575018_HELI_COIL_Side_1_higlighted[1].SetActive(true);
        snapAbleHandler = _575018_HELI_COIL_Side_1_higlighted[1].GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_4()
    {
        _575018_HELI_COIL_Side_1_higlighted[2].SetActive(true);
        snapAbleHandler = _575018_HELI_COIL_Side_1_higlighted[2].GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_5()
    {
        //rotate _DEV02704_SLIDE_HOUSE to 0 in y axis using do tween
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_DEV02704_SLIDE_HOUSE.transform.DORotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.InOutSine));
        //then again rotate to 90 in x axis and -30 in y axis
        sequence.Append(_DEV02704_SLIDE_HOUSE.transform.DORotate(new Vector3(90, -30, 0), 1f).SetEase(Ease.InOutSine));
        sequence.onComplete = () =>
        {
            sequence.Kill();
            _575018_HELI_COIL_Side_2_higlighted[0].SetActive(true);
            snapAbleHandler = _575018_HELI_COIL_Side_2_higlighted[0].GetComponent<SnapAblePartHandler>();
            snapAbleHandler.OnSnapped += Handler_OnSnapped;
            snapAbleHandler.Initialize();
        };
    }

    public void step_6()
    {
        _575018_HELI_COIL_Side_2_higlighted[1].SetActive(true);
        snapAbleHandler = _575018_HELI_COIL_Side_2_higlighted[1].GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();       
    }

    public void step_7()
    {
        _575018_HELI_COIL_Side_2_higlighted[2].SetActive(true);
        snapAbleHandler = _575018_HELI_COIL_Side_2_higlighted[2].GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }
    public void step_8()
    {
        _575018_HELI_COIL_Side_2_higlighted[3].SetActive(true);
        snapAbleHandler = _575018_HELI_COIL_Side_2_higlighted[3].GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_9()
    {
        //rotate _DEV02704_SLIDE_HOUSE to 0 in y axis using do tween
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_DEV02704_SLIDE_HOUSE.transform.DORotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.InOutSine));
        //then again rotate to 90 in x axis and -30 in y axis
        sequence.Append(_DEV02704_SLIDE_HOUSE.transform.DORotate(new Vector3(0, 40, 0), 1f).SetEase(Ease.InOutSine));
        sequence.onComplete = () =>
        {
            sequence.Kill();
            _216150_PLUG_highlighted.SetActive(true);
            snapAbleHandler = _216150_PLUG_highlighted.GetComponent<SnapAblePartHandler>();
            snapAbleHandler.OnSnapped += Handler_OnSnapped;
            snapAbleHandler.Initialize();
        };            
    }
}
