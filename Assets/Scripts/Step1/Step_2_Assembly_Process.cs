using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;


public class Step_2_Assembly_Process : StepAssemblyProcessBase
{
    public GameObject _2471384_COVER;
    public GameObject _420528_O_RING;
    public GameObject _463126_BACKUP_RING;
    public GameObject _420528_O_RING_R1;
    public GameObject _463126_BACKUP_RING_R1;    
    public GameObject _281240_DECOMPRESSION_CONE_TOOL;
    public GameObject _281132_COMPRESSION_CONE_TOOL;  
    public int totalSteps = 5;
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
        AssignPartsFromChildren();
        Debug.Log("init step 1 ass");      
        _UIManager.step_num_text.text = "<size=10>STEP:2</size>";
        _UIManager.step_Heading_Text.text = "ASSEMBLE BACKUP RING AND O-RING ON TO COVER";
        _UIManager.step_SubHeading_Text.text = "Click and generate 2471384 COVER from the panel, grab it from table and move towards highlighted part.";
        step_1();       
    }

    void AssignPartsFromChildren()
    {
        // Get all public fields of type GameObject in this class
        var fields = typeof(Step_2_Assembly_Process).GetFields(BindingFlags.Instance | BindingFlags.Public);

        foreach (Transform child in transform)
        {
            // Build the expected field name (with underscore)
            string fieldName = $"_{child.name}";
            // Find the field with this name
            var field = Array.Find(fields, f => f.Name == fieldName && f.FieldType == typeof(GameObject));
            if (field != null)
            {
                field.SetValue(this, child.gameObject);
            }
        }
        DisableAllParts();
    }

    //disable all the assembly parts 
    public void DisableAllParts()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }


// Event handler for when a part is snapped
private void Handler_OnSnapped(SnapAblePartHandler sender)
    {
        Debug.Log("Part snapped: " + sender.snapTagId);
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        currentStep++;
        StepSwitch();
    }

    //calling next step
    public void StepSwitch()
    {
        Debug.Log("Current Step: " + currentStep);
        switch (currentStep)
        {
            case 1:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281240 Decompression Cone Tool from the panel, grab it from table and move towards highlighted part.";
                step_2();
                break;
            case 2:
                _UIManager.step_SubHeading_Text.text = "Click and generate 463126 BACKUP RING from the panel, grab it from table and move towards highlighted part.";
                step_3();
                break;
            case 3:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281132 COMPRESSION CONE TOOL from the panel, grab it from table and move towards highlighted part.";
                step_4();
                break;
            case 4:
                _UIManager.step_SubHeading_Text.text = "";
                step_5_BackUpRing_Anim();
                break;
            case 5:
                _UIManager.step_SubHeading_Text.text = "Click and generate 420528 O RING  from the panel, grab it from table and move towards highlighted part.";
                step_6();
                break;
            case 6:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281132 COMPRESSION CONE TOOL from the panel, grab it from table and move towards highlighted part.";
                step_7();
                break;
            case 7:
                _UIManager.step_SubHeading_Text.text = "";
                step_8_ORing_Anim();
                break;
            case 8:
                Debug.Log("Assembly Process Completed!");
                _UIManager.step_SubHeading_Text.text = "Step 2 assembly Process completed.";
                StepCompleted();
            break;


        }
    }
    // Call this when the step is completed
    private void StepCompleted()
    {
        Debug.Log("Step_1 Completed");
        InvokeStepComplete();
    }


    public void step_1()
    {
        _2471384_COVER.SetActive(true);
        snapAbleHandler = _2471384_COVER.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }
      

    public void step_2()
    {
        _281240_DECOMPRESSION_CONE_TOOL.SetActive(true);
        snapAbleHandler = _281240_DECOMPRESSION_CONE_TOOL.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_3()
    {
        _463126_BACKUP_RING_R1.SetActive(true);
        snapAbleHandler = _463126_BACKUP_RING_R1.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_4()
    {
        _281132_COMPRESSION_CONE_TOOL.SetActive(true);
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_5_BackUpRing_Anim()
    {
        Debug.Log("play animation R_Comp_ORing");
        _animator.Play("Comp_Tool_BackUp_Ring_step2");
    }  

    public void OnComplete_step_5_BackUpRing_Anim()
    {
        _animator.enabled = false;
        Debug.Log("animation R_Comp_ORing ended");
        currentStep++;
        //reset the position and rotation of the _281132_COMPRESSION_CONE_TOOL
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL.GetComponent<SnapAblePartHandler>();
        _281132_COMPRESSION_CONE_TOOL.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL.transform.localPosition = snapAbleHandler.startPos;
        _281132_COMPRESSION_CONE_TOOL.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        snapAbleHandler.isSnapped =false;
        //reset the position and rotation of the _420561_O_RING_R1
        snapAbleHandler = _463126_BACKUP_RING_R1.GetComponent<SnapAblePartHandler>();
        _463126_BACKUP_RING_R1.SetActive(false);
        _463126_BACKUP_RING_R1.transform.localPosition = snapAbleHandler.startPos;
        _463126_BACKUP_RING_R1.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.isSnapped = false;
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        //enable the part _420561_O_RING
        _463126_BACKUP_RING.SetActive(true);
        StepSwitch();
    }

    public void step_6() 
    {
        _420528_O_RING_R1.SetActive(true);
        snapAbleHandler = _420528_O_RING_R1.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_7()
    {
        _281132_COMPRESSION_CONE_TOOL.SetActive(true);
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_8_ORing_Anim()
    {
        _animator.enabled = true;
        Debug.Log("play animation Comp_Tool_O_Ring_step2");
        _animator.Play("Comp_Tool_O_Ring_step2");
    }

    public void OnComplete_step_8_ORing_Anim()
    {
        _animator.enabled = false;
        Debug.Log("animation Comp_Tool_O_Ring_step2");
        currentStep++;
        //reset the position and rotation of the _281132_COMPRESSION_CONE_TOOL_R
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL.GetComponent<SnapAblePartHandler>();
        _281132_COMPRESSION_CONE_TOOL.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL.transform.localPosition = snapAbleHandler.startPos;
        _281132_COMPRESSION_CONE_TOOL.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        snapAbleHandler.isSnapped = false;
        //reset the position and rotation of the _452008_SEAL_RING_R1
        snapAbleHandler = _420528_O_RING_R1.GetComponent<SnapAblePartHandler>();
        _420528_O_RING_R1.SetActive(false);
        _420528_O_RING_R1.transform.localPosition = snapAbleHandler.startPos;
        _420528_O_RING_R1.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.isSnapped = false;
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        //disable the part _281240_DECOMPRESSION_CONE_TOOL_R
        _281240_DECOMPRESSION_CONE_TOOL.SetActive(false);
        //enable the part _452008_SEAL_RING
        _420528_O_RING.SetActive(true);
        StepSwitch();
    }


}
