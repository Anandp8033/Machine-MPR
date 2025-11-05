using DG.Tweening;
using System;
using TMPro;
using UnityEngine;


public class R_MPR_Step_7_Assembly_Process : StepAssemblyProcessBase
{
    public GameObject _2031179_PISTON;
    public GameObject _220129_SLIDE;
    public GameObject _220129_SLIDE_highlighted;
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
        _UIManager.step_num_text.text = "<size=10>STEP:7</size>";
        _UIManager.step_Heading_Text.text = "INSERT SLIDE IN TO PISTON.";
        _UIManager.step_SubHeading_Text.text = "Click and generate 2031179 PISTON from the panel, grab it from table and move towards highlighted part.";
        step_1();
    }


    //disable all the assembly parts 
    public void DisableAllParts()
    {
        _2031179_PISTON.SetActive(false);
        _220129_SLIDE.SetActive(false);
        _220129_SLIDE_highlighted.SetActive(false);

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
                sequence1.Append(_220129_SLIDE_highlighted.transform.DOMove(_220129_SLIDE.transform.position, 1f).SetEase(Ease.InOutSine));
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
                _UIManager.step_SubHeading_Text.text = "Click and generate 220129 SLIDE from the panel, grab it from table and move towards highlighted part.";
                step_2();
                break;
            case 2:
                _UIManager.step_SubHeading_Text.text = "Step 7 assembly Process completed.";
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
        _2031179_PISTON.SetActive(true);
        snapAbleHandler = _2031179_PISTON.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }


    public void step_2()
    {
       _220129_SLIDE_highlighted.SetActive(true);
        snapAbleHandler = _220129_SLIDE_highlighted.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }
}
