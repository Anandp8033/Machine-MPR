using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;


public class Step_1_Assembly_Process : StepAssemblyProcessBase
{

    public GameObject _2031179_PISTON;
    public GameObject _420561_O_RING;
    public GameObject _452008_SEAL_RING;
    public GameObject _420561_O_RING_R1;
    public GameObject _452008_SEAL_RING_R1;
    public GameObject _420561_O_RING_L;
    public GameObject _452008_SEAL_RING_L;
    public GameObject _420561_O_RING_L1;
    public GameObject _452008_SEAL_RING_L1;
    public GameObject _281240_DECOMPRESSION_CONE_TOOL_R;
    public GameObject _281132_COMPRESSION_CONE_TOOL_R;
    public GameObject _281240_DECOMPRESSION_CONE_TOOL_L;
    public GameObject _281132_COMPRESSION_CONE_TOOL_L;   
    public int totalSteps = 5;
    public int currentStep = 0;
    public Animator _animator;
    SnapAblePartHandler snapAbleHandler;
    private UIManager _UIManager;

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
        _UIManager.step_num_text.text = "<size=10>STEP:1</size>";
        _UIManager.step_Heading_Text.text = "ASSEMBLE,O-RING AND SEALRING ON TO PISTON";
        _UIManager.step_SubHeading_Text.text = "Click and generate 2031179 PISTON from the panel, grab it from table and move towards highlighted part."; 
        step_1();       
    }

    void AssignPartsFromChildren()
    {
        // Get all public fields of type GameObject in this class
        var fields = typeof(Step_1_Assembly_Process).GetFields(BindingFlags.Instance | BindingFlags.Public);

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
        _2031179_PISTON.SetActive(false);
        _420561_O_RING.SetActive(false);
        _452008_SEAL_RING.SetActive(false);
        _420561_O_RING_R1.SetActive(false);
        _452008_SEAL_RING_R1.SetActive(false);
        _420561_O_RING_L.SetActive(false);
        _452008_SEAL_RING_L.SetActive(false);
        _420561_O_RING_L1.SetActive(false);
        _452008_SEAL_RING_L1.SetActive(false);
        _281240_DECOMPRESSION_CONE_TOOL_R.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL_R.SetActive(false);
        _281240_DECOMPRESSION_CONE_TOOL_L.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL_L.SetActive(false);
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
                _UIManager.step_SubHeading_Text.text = "Click and generate 420561 O RING from the panel, grab it from table and move towards highlighted part."; 
                step_3();
                break;
            case 3:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281132 Compression Cone Tool from the panel, grab it from table and move towards highlighted part.";
                step_4();
                break;
            case 4:
                _UIManager.step_SubHeading_Text.text = "";
                step_5_Comp_Tool_ORing_R();
                break;
            case 5:
                _UIManager.step_SubHeading_Text.text = "Click and generate 452008 Seal Ring from the panel, grab it from table and move towards highlighted part.";
                step_6();
                break;
            case 6:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281132 Compression Cone Tool from the panel, grab it from table and move towards highlighted part.";
                step_7();
                break;
            case 7:
                _UIManager.step_SubHeading_Text.text = "";
                step_8_Comp_Tool_Seal_Ring_R(); 
                break;
            case 8:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281240 Decompression Cone Tool from the panel, grab it from table and move towards highlighted part.";
                step_9();
                break;
            case 9:
                _UIManager.step_SubHeading_Text.text = "Click and generate 420561 O RING from the panel, grab it from table and move towards highlighted part.";
                step_10();
                break;
            case 10:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281132 Compression Cone Tool from the panel, grab it from table and move towards highlighted part.";
                step_11();
                break;
            case 11:
                _UIManager.step_SubHeading_Text.text = "";
                step_12_Comp_Tool_ORing_L(); 
                break;
            case 12:
                _UIManager.step_SubHeading_Text.text = "Click and generate 452008 Seal Ring from the panel, grab it from table and move towards highlighted part.";
                Step_13();
                break;
            case 13:
                _UIManager.step_SubHeading_Text.text = "Click and generate Compression Cone Tool from the panel, grab it from table and move towards highlighted part.";
                step_14();
                break;
            case 14:
                _UIManager.step_SubHeading_Text.text = "";
                Step_15_Comp_Tool_SealRing_L(); 
                break;
            case 15:
                Debug.Log("Assembly Process Completed!");
                _UIManager.step_SubHeading_Text.text = "Step 1 assembly Process completed.";
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
        _2031179_PISTON.SetActive(true);
        snapAbleHandler = _2031179_PISTON.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }
      

    public void step_2()
    {
        _281240_DECOMPRESSION_CONE_TOOL_R.SetActive(true);
        snapAbleHandler = _281240_DECOMPRESSION_CONE_TOOL_R.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_3()
    {
        _420561_O_RING_R1.SetActive(true);
        snapAbleHandler = _420561_O_RING_R1.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_4()
    {
        _281132_COMPRESSION_CONE_TOOL_R.SetActive(true);
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL_R.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_5_Comp_Tool_ORing_R()
    {
        Debug.Log("play animation R_Comp_ORing");
        _animator.Play("Comp_Tool_ORing_R");
    }

    public void OnComplete_R_Comp_ORing_Animation()
    {
        _animator.enabled = false;
        Debug.Log("animation R_Comp_ORing ended");
        currentStep++;
        //reset the position and rotation of the _281132_COMPRESSION_CONE_TOOL_R
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL_R.GetComponent<SnapAblePartHandler>();
        _281132_COMPRESSION_CONE_TOOL_R.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL_R.transform.localPosition = snapAbleHandler.startPos;
        _281132_COMPRESSION_CONE_TOOL_R.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        snapAbleHandler.isSnapped =false;
        //reset the position and rotation of the _420561_O_RING_R1
        snapAbleHandler = _420561_O_RING_R1.GetComponent<SnapAblePartHandler>();
        _420561_O_RING_R1.SetActive(false);
        _420561_O_RING_R1.transform.localPosition = snapAbleHandler.startPos;
        _420561_O_RING_R1.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.isSnapped = false;
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        //enable the part _420561_O_RING
        _420561_O_RING.SetActive(true);
        StepSwitch();
    }

    public void step_6() 
    {
        _452008_SEAL_RING_R1.SetActive(true);
        snapAbleHandler = _452008_SEAL_RING_R1.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_7()
    {
        _281132_COMPRESSION_CONE_TOOL_R.SetActive(true);
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL_R.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_8_Comp_Tool_Seal_Ring_R()
    {
        _animator.enabled = true;
        Debug.Log("play animation R_Comp_Seal_Ring");
        _animator.Play("Comp_Tool_Seal_Ring_R");
    }

    public void OnComplete_R_Comp_SealRing_Animation()
    {
        _animator.enabled = false;
        Debug.Log("animation R_Comp_Seal_Ring ended");
        currentStep++;
        //reset the position and rotation of the _281132_COMPRESSION_CONE_TOOL_R
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL_R.GetComponent<SnapAblePartHandler>();
        _281132_COMPRESSION_CONE_TOOL_R.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL_R.transform.localPosition = snapAbleHandler.startPos;
        _281132_COMPRESSION_CONE_TOOL_R.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        snapAbleHandler.isSnapped = false;
        //reset the position and rotation of the _452008_SEAL_RING_R1
        snapAbleHandler = _452008_SEAL_RING_R1.GetComponent<SnapAblePartHandler>();
        _452008_SEAL_RING_R1.SetActive(false);
        _452008_SEAL_RING_R1.transform.localPosition = snapAbleHandler.startPos;
        _452008_SEAL_RING_R1.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.isSnapped = false;
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        //disable the part _281240_DECOMPRESSION_CONE_TOOL_R
        _281240_DECOMPRESSION_CONE_TOOL_R.SetActive(false);
        //enable the part _452008_SEAL_RING
        _452008_SEAL_RING.SetActive(true);
        StepSwitch();
    }

    private void step_9()
    {
        Debug.Log("step 9");
        _281240_DECOMPRESSION_CONE_TOOL_L.SetActive(true);
        snapAbleHandler = _281240_DECOMPRESSION_CONE_TOOL_L.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    private void step_10()
    {
        Debug.Log("step 10");
        _420561_O_RING_L1.SetActive(true);
        snapAbleHandler = _420561_O_RING_L1.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    private void step_11()
    {
        _281132_COMPRESSION_CONE_TOOL_L.SetActive(true);
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL_L.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    private void step_12_Comp_Tool_ORing_L()
    {
        _animator.enabled = true;
        Debug.Log("play animation L_Comp_ORing");
        _animator.Play("Comp_Tool_ORing_L");
    }

    public void OnComplete_L_Comp_ORing_Animation()
    {
        _animator.enabled = false;
        Debug.Log("animation R_Comp_Seal_Ring ended");
        currentStep++;
        //reset the position and rotation of the _281132_COMPRESSION_CONE_TOOL_L
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL_L.GetComponent<SnapAblePartHandler>();
        _281132_COMPRESSION_CONE_TOOL_L.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL_L.transform.localPosition = snapAbleHandler.startPos;
        _281132_COMPRESSION_CONE_TOOL_L.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        snapAbleHandler.isSnapped = false;
        //reset the position and rotation of the _420561_O_RING_L1
        snapAbleHandler = _420561_O_RING_L1.GetComponent<SnapAblePartHandler>();
        _420561_O_RING_L1.SetActive(false);
        _420561_O_RING_L1.transform.localPosition = snapAbleHandler.startPos;
        _420561_O_RING_L1.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot); 
        snapAbleHandler.isSnapped = false;
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        //enable the part _452008_SEAL_RING
        _420561_O_RING_L.SetActive(true);
        StepSwitch();
    }

    private void Step_13()
    {
        _452008_SEAL_RING_L1.SetActive(true);
        snapAbleHandler = _452008_SEAL_RING_L1.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    private void step_14()
    {
        _281132_COMPRESSION_CONE_TOOL_L.SetActive(true);
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL_L.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    private void Step_15_Comp_Tool_SealRing_L()
    {
        _animator.enabled = true;
        Debug.Log("play animation L_Comp_Seal_Ring");
        _animator.Play("Comp_Tool_Seal_Ring_L");
    }

    public void OnComplete_L_Comp_SealRing_Animation()
    {
        _animator.enabled = false;
        Debug.Log("animation L_Comp_Seal_Ring ended");
        currentStep++;
        //reset the position and rotation of the _281132_COMPRESSION_CONE_TOOL_L
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL_L.GetComponent<SnapAblePartHandler>();
        _281132_COMPRESSION_CONE_TOOL_L.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL_L.transform.localPosition = snapAbleHandler.startPos;
        _281132_COMPRESSION_CONE_TOOL_L.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        snapAbleHandler.isSnapped = false;
        //reset the position and rotation of the _452008_SEAL_RING_L1
        snapAbleHandler = _452008_SEAL_RING_L1.GetComponent<SnapAblePartHandler>();
        _452008_SEAL_RING_L1.SetActive(false);
        _452008_SEAL_RING_L1.transform.localPosition = snapAbleHandler.startPos;
        _452008_SEAL_RING_L1.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.isSnapped = false;
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        //disable the part _281240_DECOMPRESSION_CONE_TOOL_L
        _281240_DECOMPRESSION_CONE_TOOL_L.SetActive(false);
        //enable the part _452008_SEAL_RING
        _452008_SEAL_RING_L.SetActive(true);
        StepSwitch();
    }

}
