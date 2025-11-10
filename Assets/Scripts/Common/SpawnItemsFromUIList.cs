using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpawnItemsFromUIList : XRBaseInteractable ///MonoBehaviour,IPointerExitHandler, IPointerEnterHandler
{

    public GameObject prefabToSpawn;
    public Transform spawnOffset;
    private GameObject spawnedObject;
    public Vector3 Parts_Size;
    private IXRSelectInteractor currentInteractor;


    public Image targetImage;
    public float glowIntensity = 0.8f;

    public GameObject TableTransform;

    /// <summary>
    /// Global(process-wide) lock
    /// </summary>
    private static bool s_isSnapping = false;
    public static bool IsSnapping => s_isSnapping;
    public static void SetSnapping(bool value) => s_isSnapping = value;


    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Debug.Log("VR Pointer Enter");
    //    if (targetImage != null)
    //        targetImage.material.SetFloat("_GlowIntensity", glowIntensity);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Debug.Log("VR Pointer Exit");
    //    if (targetImage != null)
    //        targetImage.material.SetFloat("_GlowIntensity", 0f);
    //}

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log("VR Hover Enter");
        if (targetImage != null)
            targetImage.material.SetFloat("_GlowIntensity", glowIntensity);
    }
    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log("VR Hover Exit");
        if (targetImage != null)
            targetImage.material.SetFloat("_GlowIntensity", 0f);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        // Cast to IXRSelectInteractor
        IXRSelectInteractor currentInteractor = args.interactorObject as IXRSelectInteractor;
        if (IsSnapping)
        {
            Debug.LogWarning("Spawn blocked: a snap is in progress (global).");
            interactionManager.SelectExit(currentInteractor, this);
            return;
        }
        base.OnSelectEntered(args);
        Debug.Log("VR Button Selected");
        //Clear previous gameobject under table if same part is already present then destroy it , only destroy same gameobject not other gameobject
        foreach (Transform child in TableTransform.transform)
        {
            if (child.transform.GetComponent<PartAttribute>())
            {
                Destroy(child.gameObject);
            }
        }

        // Deselect the button itself

        if (currentInteractor.hasSelection && currentInteractor.keepSelectedTargetValid == this)
        {
            interactionManager.SelectExit(currentInteractor, this);
        }

        // Spawn the object in front of the button
        spawnedObject = Instantiate(prefabToSpawn, spawnOffset.position, Quaternion.identity);
        if (Parts_Size != Vector3.zero)
        {
            spawnedObject.transform.localScale = Parts_Size;
        }
        spawnedObject.transform.parent = TableTransform.transform;

        // Ensure the spawned object has XRGrabInteractable
        XRGrabInteractable grabInteractable = spawnedObject.GetComponent<XRGrabInteractable>();

        if (grabInteractable != null && currentInteractor != null)
        {

            // Match interaction layer
            grabInteractable.interactionLayers = interactionLayers;

            // Set attach transform to controller's attach point
            if (currentInteractor is XRRayInteractor rayInteractor)
            {
                grabInteractable.attachTransform = rayInteractor.attachTransform;
            }

            // Auto-grab happens because the interactor is still selecting
            StartCoroutine(DelayedSelect(currentInteractor, grabInteractable));
            // Subscribe to its select exit event
            //grabInteractable.selectExited.AddListener(OnSpawnedObjectReleased);
        }

    }

    private IEnumerator DelayedSelect(IXRSelectInteractor interactor, XRGrabInteractable interactable)
    {
        yield return new WaitForSeconds(0.001f); // Wait one frame
        interactionManager.SelectEnter(interactor, interactable);
    }

    private void OnSpawnedObjectReleased(SelectExitEventArgs args)
    {
        Debug.Log("Spawned object released, destroying...");
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;


        }
    }
}