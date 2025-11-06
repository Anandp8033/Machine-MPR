using System;
using Unity.XR.CompositionLayers;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.CompositionLayers;

public class VRControllerButtonEvents : MonoBehaviour
{
    public GameObject XROrigin;
    public GameObject MetaPassThroughLayer;

    public Material defaultSkybox;  
    private bool isPassthrough = false;

    public GameObject sceneAssets;
    public float startDistance = 2f;
    [SerializeField]
    private Vector3 startPosition;
    private Quaternion startRotation;
    private XROrigin xrOrigin;    

    public InputActionAsset inputActions;

    void OnEnable()
    {
        // Left Hand Controller
        var actionMapLeft = inputActions.FindActionMap("XRI Left Interaction");
        var buttonSelectAction = actionMapLeft.FindAction("Select");
        buttonSelectAction.performed += OnGripLeftButtonPressed;
        buttonSelectAction.canceled += OnGripLeftButtonReleased;
        buttonSelectAction.Enable();

        var buttonLeftTriggerAction = actionMapLeft.FindAction("Activate");
        buttonLeftTriggerAction.performed += OnTriggerLeftButtonPressed;
        buttonLeftTriggerAction.canceled += OnTriggerLeftButtonReleased;
        buttonLeftTriggerAction.Enable();

        var XSelectAction = actionMapLeft.FindAction("X_Button");
        XSelectAction.performed += On_X_ButtonPressed;
        XSelectAction.canceled += On_X_ButtonReleased;
        XSelectAction.Enable();

        var YSelectAction = actionMapLeft.FindAction("Y_Button");
        YSelectAction.performed += On_Y_ButtonPressed;
        YSelectAction.canceled += On_Y_ButtonReleased;
        YSelectAction.Enable();

        // Right Hand Controller
        var actionMapSelectRight = inputActions.FindActionMap("XRI Right Interaction");
        var buttonActionSelectRight = actionMapSelectRight.FindAction("Select");
        buttonActionSelectRight.performed += OnGripRightButtonPressed;
        buttonActionSelectRight.canceled += OnGripRightButtonReleased;
        buttonActionSelectRight.Enable();

        var buttonRightTriggerAction = actionMapSelectRight.FindAction("Activate");
        buttonRightTriggerAction.performed += OnTriggerRightButtonPressed;
        buttonRightTriggerAction.canceled += OnTriggerRightButtonReleased;
        buttonRightTriggerAction.Enable();

        var ASelectAction = actionMapSelectRight.FindAction("A_Button");
        ASelectAction.performed += On_A_ButtonPressed;
        ASelectAction.canceled += On_A_ButtonReleased;
        ASelectAction.Enable();

        var BSelectAction = actionMapSelectRight.FindAction("B_Button");
        BSelectAction.performed += On_B_ButtonPressed;
        BSelectAction.canceled += On_B_ButtonReleased;
        BSelectAction.Enable();

    }

    void OnDisable()
    {
        // Left Hand Controller
        var LeftactionMap = inputActions.FindActionMap("XRI Left Interaction");
        var buttonAction = LeftactionMap.FindAction("Select");
        buttonAction.performed -= OnGripLeftButtonPressed;
        buttonAction.canceled -= OnGripLeftButtonReleased;
        buttonAction.Disable();

        var buttonLeftTriggerAction = LeftactionMap.FindAction("Activate");
        buttonLeftTriggerAction.performed -= OnTriggerLeftButtonPressed;
        buttonLeftTriggerAction.canceled -= OnTriggerLeftButtonReleased;
        buttonLeftTriggerAction.Enable();

        var XSelectAction = LeftactionMap.FindAction("X_Button");
        XSelectAction.performed -= On_X_ButtonPressed;
        XSelectAction.canceled -= On_X_ButtonReleased;
        XSelectAction.Enable();

        var YSelectAction = LeftactionMap.FindAction("Y_Button");
        YSelectAction.performed += On_Y_ButtonPressed;
        YSelectAction.canceled += On_Y_ButtonReleased;
        YSelectAction.Enable();

        // Right Hand Controller
        var actionMapRightGrip = inputActions.FindActionMap("XRI Right Interaction");
        var buttonActionRight = actionMapRightGrip.FindAction("Select");
        buttonActionRight.performed -= OnGripRightButtonPressed;
        buttonActionRight.canceled -= OnGripRightButtonReleased;
        buttonActionRight.Disable();

        var buttonRightTriggerAction = actionMapRightGrip.FindAction("Activate");
        buttonRightTriggerAction.performed -= OnTriggerRightButtonPressed;
        buttonRightTriggerAction.canceled -= OnTriggerRightButtonReleased;
        buttonRightTriggerAction.Enable();

        var ASelectAction = actionMapRightGrip.FindAction("A_Button");
        ASelectAction.performed -= On_A_ButtonPressed;
        ASelectAction.canceled -= On_A_ButtonReleased;
        ASelectAction.Enable();

        var BSelectAction = actionMapRightGrip.FindAction("B_Button");
        BSelectAction.performed -= On_B_ButtonPressed;
        BSelectAction.canceled -= On_B_ButtonReleased;
        BSelectAction.Enable();

    }

    void Start()
    {
        xrOrigin = XROrigin.GetComponent<XROrigin>();
        startPosition = (sceneAssets.transform.position - sceneAssets.transform.forward) * startDistance;
        Invoke("RecenterToAsset", 0.2f);
    }

    private void On_A_ButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("A Button Pressed");
        RecenterToAsset();
    }
    private void On_A_ButtonReleased(InputAction.CallbackContext context)
    {
        //Debug.Log("A Button Released");
    }
    private void On_B_ButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("B Button Pressed");
        ToggleMode();
    }
    private void On_B_ButtonReleased(InputAction.CallbackContext context)
    {
        //Debug.Log("B Button Released");
    }

    private void On_X_ButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("X Button Pressed");
    }
    private void On_X_ButtonReleased(InputAction.CallbackContext context)
    {
        //Debug.Log("X Button Released");
    }

    private void On_Y_ButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("Y Button Pressed");
    }
    private void On_Y_ButtonReleased(InputAction.CallbackContext context)
    {
       //Debug.Log("Y Button Released");
    }


    private void OnGripLeftButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("Grip Button left Pressed");
    }

    private void OnGripLeftButtonReleased(InputAction.CallbackContext context)
    {
        //Debug.Log("Grip Button Left Released");
    }

    private void OnGripRightButtonPressed(InputAction.CallbackContext context)
    {
       // Debug.Log("Grip Button Right Pressed");
    }

    private void OnGripRightButtonReleased(InputAction.CallbackContext context)
    {
        //Debug.Log("Grip Button Right Released");
    }

    private void OnTriggerLeftButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("Trigger Button left Pressed");
    }

    private void OnTriggerLeftButtonReleased(InputAction.CallbackContext context)
    {
       // Debug.Log("Trigger Button Left Released");
    }

    private void OnTriggerRightButtonPressed(InputAction.CallbackContext context)
    {
       // Debug.Log("Trigger Button Right Pressed");
    }

    private void OnTriggerRightButtonReleased(InputAction.CallbackContext context)
    {
        //Debug.Log("Trigger Button Right Released");
    }


    // Call this method to recenter XR Origin so user faces the asset.
    /// </summary>
    public void RecenterToAsset()
    {
        xrOrigin.MatchOriginUpOriginForward(Vector3.up, (sceneAssets.transform.position - startPosition).normalized);
        xrOrigin.MoveCameraToWorldLocation(startPosition);
        XROrigin.transform.position = new Vector3(startPosition.x, 0.5f, startPosition.z);

    }

    /// <summary>
    /// Optional: Check if user is facing asset and auto-recenter if needed.
    /// </summary>
    public void AutoRecenterIfNotFacing()
    {
        Transform cameraTransform = xrOrigin.Camera.transform;
        Vector3 toAsset = (sceneAssets.transform.position - cameraTransform.position).normalized;
        float angle = Vector3.Angle(cameraTransform.forward, toAsset);

        if (angle > 30f) // If user is not facing asset (threshold)
        {
            RecenterToAsset();
        }
    }

    public void ToggleMode()
    {
        isPassthrough = !isPassthrough;
        MetaPassThroughLayer.SetActive(isPassthrough);
        RenderSettings.skybox = isPassthrough ? null : defaultSkybox;
        Camera.main.clearFlags = isPassthrough ? CameraClearFlags.SolidColor : CameraClearFlags.Skybox;
        //also change SolidColor backgroud alpha to 0 when in passthrough
        if (isPassthrough)
        {
            Camera.main.backgroundColor = new Color(0, 0, 0, 0);
        }
        else
        {
            //also change SolidColor backgroud black alpha to 1 when in passthrough
            Camera.main.backgroundColor = new Color(0, 0, 0, 1);
        }
    }

}
