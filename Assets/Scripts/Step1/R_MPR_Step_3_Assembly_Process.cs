using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;


public class R_MPR_Step_3_Assembly_Process : StepAssemblyProcessBase
{
    public GameObject _2471383_COVER;
    public GameObject _420512_O_RING;
    public GameObject _463128_BACKUP_RING;
    public GameObject _420512_O_RING_R;
    public GameObject _463128_BACKUP_RING_R;
    public GameObject _420512_O_RING_L;
    public GameObject _463128_BACKUP_RING_L;
    public GameObject _420512_O_RING_L1;
    public GameObject _463128_BACKUP_RING_L1;
    public GameObject _281241_DECOMPRESSION_CONE_TOOL;
    public GameObject _281243_DECOMPRESSION_CONE_TOOL;
    public GameObject _281132_COMPRESSION_CONE_TOOL;
    public int totalSteps = 5;
    public int currentStep = 0;
    public Animator _animator;
    SnapAblePartHandler snapAbleHandler;
    private UIManager _UIManager;

    public event Action Onstep_2Complete;

    private void Awake()
    {
        AssignPartsFromChildren();
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
        _UIManager.step_num_text.text = "<size=10>STEP:3</size>";
        _UIManager.step_Heading_Text.text = "ASSEMBLE BACKUP RING AND O-RING ON TO COVER";
        _UIManager.step_SubHeading_Text.text = "Grab 2471383 COVER from the panel and move towards highlighted part.";
        step_1();
    }

    void AssignPartsFromChildren()
    {
        // Get all public fields of type GameObject in this class
        var fields = typeof(R_MPR_Step_3_Assembly_Process).GetFields(BindingFlags.Instance | BindingFlags.Public);

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
        _2471383_COVER.SetActive(false);
        _420512_O_RING.SetActive(false);
        _463128_BACKUP_RING.SetActive(false);
        _420512_O_RING_R.SetActive(false);
        _463128_BACKUP_RING_R.SetActive(false);
        _420512_O_RING_L.SetActive(false);
        _463128_BACKUP_RING_L.SetActive(false);
        _420512_O_RING_L1.SetActive(false);
        _463128_BACKUP_RING_L1.SetActive(false);
        _281241_DECOMPRESSION_CONE_TOOL.SetActive(false);
        _281243_DECOMPRESSION_CONE_TOOL.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL.SetActive(false);
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
                _UIManager.step_SubHeading_Text.text = "Click and generate 281241 Decompression Cone Tool from the panel, grab it from table and move towards highlighted part.";
                step_2();
                break;
            case 2:
                _UIManager.step_SubHeading_Text.text = "Click and generate 420512 O RING from the panel, grab it from table and move towards highlighted part.";
                step_3();
                break;
            case 3:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281132 COMPRESSION CONE TOOL from the panel, grab it from table and move towards highlighted part.";
                step_4();
                break;
            case 4:
                _UIManager.step_SubHeading_Text.text = "";
                step_5_O_Ring_Anim();
                break;
            case 5:
                _UIManager.step_SubHeading_Text.text = "Click and generate 463128 BACKUP RING  from the panel, grab it from table and move towards highlighted part.";
                step_6();
                break;
            case 6:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281132 COMPRESSION CONE TOOL from the panel, grab it from table and move towards highlighted part.";
                step_7();
                break;
            case 7:
                _UIManager.step_SubHeading_Text.text = "";
                step_8_BackUP_Ring_Anim();
                break;
            case 8:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281243 Decompression Cone Tool from the panel, grab it from table and move towards highlighted part.";
                step_9();
                break;
            case 9:
                _UIManager.step_SubHeading_Text.text = "Click and generate 463128 BACKUP RING from the panel, grab it from table and move towards highlighted part.";
                step_10();
                break;
            case 10:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281132 COMPRESSION CONE TOOL from the panel, grab it from table and move towards highlighted part.";
                step_10_1();
                break;
            case 11:
                _UIManager.step_SubHeading_Text.text = "";
                step_11_BackUP_Ring_L_Anim();
                break;
            case 12:
                _UIManager.step_SubHeading_Text.text = "Click and generate 420512 O RING from the panel, grab it from table and move towards highlighted part.";
                step_13();
                break;
            case 13:
                _UIManager.step_SubHeading_Text.text = "Click and generate 281132 COMPRESSION CONE TOOL from the panel, grab it from table and move towards highlighted part.";
                step_10_1();
                break;
            case 14:
                _UIManager.step_SubHeading_Text.text = "";
                step_14_O_Ring_L_Anim();
                break;
            case 15:
                Debug.Log("Assembly Process Completed!");
                _UIManager.step_SubHeading_Text.text = "Step 3 assembly Process completed.";
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
        _2471383_COVER.SetActive(true);
        snapAbleHandler = _2471383_COVER.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }


    public void step_2()
    {
        _281241_DECOMPRESSION_CONE_TOOL.SetActive(true);
        snapAbleHandler = _281241_DECOMPRESSION_CONE_TOOL.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_3()
    {
        _420512_O_RING_R.SetActive(true);
        snapAbleHandler = _420512_O_RING_R.GetComponent<SnapAblePartHandler>();
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

    public void step_5_O_Ring_Anim()
    {
        // Get the RuntimeAnimatorController
        RuntimeAnimatorController controller = _animator.runtimeAnimatorController;
        // Find the animation clip by name
        AnimationClip targetClip = null;
        foreach (var clip in controller.animationClips)
        {
            if (clip.name == "Comp_Tool_O_Ring_step3")
            {
                targetClip = clip;
                break;
            }
        }

        if (targetClip != null)
        {
            // Clear existing events to avoid duplicates
            targetClip.events = new AnimationEvent[0];

            // Create and add the event
            AnimationEvent animEvent = new AnimationEvent();
            animEvent.time = targetClip.length;
            animEvent.functionName = "Trigger_Step_5_O_Ring_Anim";

            targetClip.AddEvent(animEvent);
        }

        // Play the animation
        _animator.Play("Comp_Tool_O_Ring_step3");
    }

    // Called by animation event
    public void Trigger_Step_5_O_Ring_Anim()
    {
        Invoke(nameof(OnComplete_step_5_O_Ring_Anim), 1);
    }


    public void OnComplete_step_5_O_Ring_Anim()
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
        snapAbleHandler.isSnapped = false;
        //reset the position and rotation of the _420561_O_RING_R1
        snapAbleHandler = _420512_O_RING_R.GetComponent<SnapAblePartHandler>();
        _420512_O_RING_R.SetActive(false);
        _420512_O_RING_R.transform.GetComponent<BoxCollider>().enabled = false;
        _420512_O_RING_R.transform.localPosition = snapAbleHandler.startPos;
        _420512_O_RING_R.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        _420512_O_RING_R.transform.GetComponent<SnapAblePartHandler>().enabled = false;
        snapAbleHandler.isSnapped = false;
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;

        //enable the part _420561_O_RING
        _420512_O_RING.SetActive(true);
        StepSwitch();
    }

    public void step_6()
    {
        _463128_BACKUP_RING_R.SetActive(true);
        snapAbleHandler = _463128_BACKUP_RING_R.GetComponent<SnapAblePartHandler>();
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

    public void step_8_BackUP_Ring_Anim()
    {
        _animator.enabled = true;
        // Get the RuntimeAnimatorController
        RuntimeAnimatorController controller = _animator.runtimeAnimatorController;
        // Find the animation clip by name
        AnimationClip targetClip = null;
        foreach (var clip in controller.animationClips)
        {
            if (clip.name == "Comp_Tool_BackUp_Ring_step3")
            {
                targetClip = clip;
                break;
            }
        }

        if (targetClip != null)
        {
            // Clear existing events to avoid duplicates
            targetClip.events = new AnimationEvent[0];

            // Create and add the event
            AnimationEvent animEvent = new AnimationEvent();
            animEvent.time = targetClip.length;
            animEvent.functionName = "Trigger_Step_8_BackUP_Ring_Anim";

            targetClip.AddEvent(animEvent);
        }
        _animator.Play("Comp_Tool_BackUp_Ring_step3");
    }

    // Called by animation event
    public void Trigger_Step_8_BackUP_Ring_Anim()
    {
        Invoke(nameof(OnComplete_step_8_BackUP_Ring_Anim), 1);
    }

    public void OnComplete_step_8_BackUP_Ring_Anim()
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
        snapAbleHandler = _463128_BACKUP_RING_R.GetComponent<SnapAblePartHandler>();
        _463128_BACKUP_RING_R.SetActive(false);
        _463128_BACKUP_RING_R.transform.GetComponent<BoxCollider>().enabled = false;
        _463128_BACKUP_RING_R.transform.localPosition = snapAbleHandler.startPos;
        _463128_BACKUP_RING_R.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        _463128_BACKUP_RING_R.transform.GetComponent<SnapAblePartHandler>().enabled = false;
        snapAbleHandler.isSnapped = false;
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        //disable the part _281240_DECOMPRESSION_CONE_TOOL_R
        _281241_DECOMPRESSION_CONE_TOOL.SetActive(false);
        //enable the part _452008_SEAL_RING
        _463128_BACKUP_RING.SetActive(true);
        StepSwitch();
    }
    public void step_9()
    {
        _281243_DECOMPRESSION_CONE_TOOL.transform.localPosition = new Vector3(-0.312f, 0, 0);
        _281243_DECOMPRESSION_CONE_TOOL.SetActive(true);
        snapAbleHandler = _281243_DECOMPRESSION_CONE_TOOL.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_10()
    {
        _463128_BACKUP_RING_L1.SetActive(true);
        snapAbleHandler = _463128_BACKUP_RING_L1.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }
    public void step_10_1()
    {
        _281132_COMPRESSION_CONE_TOOL.SetActive(true);
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_11_BackUP_Ring_L_Anim()
    {
        _animator.enabled = true;
        // Get the RuntimeAnimatorController
        RuntimeAnimatorController controller = _animator.runtimeAnimatorController;
        // Find the animation clip by name
        AnimationClip targetClip = null;
        foreach (var clip in controller.animationClips)
        {
            if (clip.name == "Comp_Tool_BackUp_Ring_L_step3")
            {
                targetClip = clip;
                break;
            }
        }

        if (targetClip != null)
        {
            // Clear existing events to avoid duplicates
            targetClip.events = new AnimationEvent[0];

            // Create and add the event
            AnimationEvent animEvent = new AnimationEvent();
            animEvent.time = targetClip.length;
            animEvent.functionName = "Trigger_Step_11_BackUP_Ring_L_Anim";

            targetClip.AddEvent(animEvent);
        }
        _animator.Play("Comp_Tool_BackUp_Ring_L_step3");
    }

    // Called by animation event
    public void Trigger_Step_11_BackUP_Ring_L_Anim()
    {
        Invoke(nameof(OnComplete_step_11_BackUP_Ring_L_Anim), 1);
    }

    public void OnComplete_step_11_BackUP_Ring_L_Anim()
    {
        _animator.enabled = false;
        currentStep++;
        //reset the position and rotation of the _281132_COMPRESSION_CONE_TOOL_R
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL.GetComponent<SnapAblePartHandler>();
        _281132_COMPRESSION_CONE_TOOL.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL.transform.localPosition = snapAbleHandler.startPos;
        _281132_COMPRESSION_CONE_TOOL.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        snapAbleHandler.isSnapped = false;
        //reset the position and rotation of the _452008_SEAL_RING_R1
        snapAbleHandler = _463128_BACKUP_RING_L1.GetComponent<SnapAblePartHandler>();
        _463128_BACKUP_RING_L1.SetActive(false);
        _463128_BACKUP_RING_L1.transform.GetComponent<BoxCollider>().enabled = false;
        _463128_BACKUP_RING_L1.transform.localPosition = snapAbleHandler.startPos;
        _463128_BACKUP_RING_L1.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        _463128_BACKUP_RING_L1.transform.GetComponent<SnapAblePartHandler>().enabled = false;
        snapAbleHandler.isSnapped = false;
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        //enable the part _452008_SEAL_RING
        _463128_BACKUP_RING_L.SetActive(true);
        StepSwitch();
    }

    //here compression tool is geting called from step 10_1

    public void step_13()
    {
        _420512_O_RING_L1.SetActive(true);
        snapAbleHandler = _420512_O_RING_L1.GetComponent<SnapAblePartHandler>();
        snapAbleHandler.OnSnapped += Handler_OnSnapped;
        snapAbleHandler.Initialize();
    }

    public void step_14_O_Ring_L_Anim()
    {
        _animator.enabled = true;
        // Get the RuntimeAnimatorController
        RuntimeAnimatorController controller = _animator.runtimeAnimatorController;
        // Find the animation clip by name
        AnimationClip targetClip = null;
        foreach (var clip in controller.animationClips)
        {
            if (clip.name == "Comp_Tool_O_Ring_L_step3")
            {
                targetClip = clip;
                break;
            }
        }

        if (targetClip != null)
        {
            // Clear existing events to avoid duplicates
            targetClip.events = new AnimationEvent[0];

            // Create and add the event
            AnimationEvent animEvent = new AnimationEvent();
            animEvent.time = targetClip.length;
            animEvent.functionName = "Trigger_step_14_O_Ring_L_Anim";

            targetClip.AddEvent(animEvent);
        }
        _animator.Play("Comp_Tool_O_Ring_L_step3");
    }

    // Called by animation event
    public void Trigger_step_14_O_Ring_L_Anim()
    {
        Invoke(nameof(OnComplete_step_11_BackUP_Ring_L_Anim), 1);
    }

    public void OnComplete_step_14_O_Ring_L_Anim()
    {
        _animator.enabled = false;
        currentStep++;
        //reset the position and rotation of the _281132_COMPRESSION_CONE_TOOL_R
        snapAbleHandler = _281132_COMPRESSION_CONE_TOOL.GetComponent<SnapAblePartHandler>();
        _281132_COMPRESSION_CONE_TOOL.SetActive(false);
        _281132_COMPRESSION_CONE_TOOL.transform.localPosition = snapAbleHandler.startPos;
        _281132_COMPRESSION_CONE_TOOL.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        snapAbleHandler.isSnapped = false;
        //reset the position and rotation of the _452008_SEAL_RING_R1
        snapAbleHandler = _420512_O_RING_L1.GetComponent<SnapAblePartHandler>();
        _420512_O_RING_L1.SetActive(false);
        _420512_O_RING_L1.transform.localPosition = snapAbleHandler.startPos;
        _420512_O_RING_L1.transform.localRotation = Quaternion.Euler(snapAbleHandler.startRot);
        snapAbleHandler.isSnapped = false;
        snapAbleHandler.OnSnapped -= Handler_OnSnapped;
        //disable the part _281240_DECOMPRESSION_CONE_TOOL_R
        _281243_DECOMPRESSION_CONE_TOOL.SetActive(false);
        //enable the part _452008_SEAL_RING
        _420512_O_RING_L.SetActive(true);
        StepSwitch();
    }
}
