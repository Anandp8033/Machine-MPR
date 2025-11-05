using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PartAttribute : MonoBehaviour
{
    public string partID;
    public GameObject PartNamelabel;
    public Renderer partRenderer;
    private GameObject labelInstance;
    private Camera mainCamera;
    public Vector3 startPos;
    public Vector3 startRot;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.eulerAngles;
        mainCamera = Camera.main;
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.hoverEntered.AddListener((XRRig) => OnPointerEnter());
        grabInteractable.hoverExited.AddListener((XRRig) => OnPointerExit());

        //get part renderer if not assigned then look in children whith same partID name having MeshRenderer component, there could be multiple children with same name so find that one having MeshRenderer component attached
        if (partRenderer == null)
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.name == partID && child.GetComponent<MeshRenderer>() != null)
                {
                    partRenderer = child.GetComponent<Renderer>();
                    if (partRenderer != null)
                    {
                        break;
                    }
                }
            }
        }

        ShowPartLabel();
    }

    //instantiate the part name label and set its text to the partID
    //void ShowPartlabel()
    //{
    //    PartNamelabel = Instantiate(PartNamelabel, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
    //    PartNamelabel.GetComponentInChildren<TextMeshProUGUI>().text = partID;
    //    PartNamelabel.transform.SetParent(transform);
    //    PartNamelabel.SetActive(false);
    //}

    ////when In VR mode when this part is getting pointed at, show the part name label
    //public void OnPointerEnter()
    //{
    //    //always positon above the part in Y axis haveing offset of 0.2f
    //    //PartNamelabel.transform.position = transform.position + new Vector3(0, 0.2f, 0);
    //    PartNamelabel.SetActive(true);
    //}

    ////when In VR mode when this part is not getting pointed at, hide the part name label
    //public void OnPointerExit()
    //{
    //    PartNamelabel.SetActive(false);
    //}


    void ShowPartLabel()
    {
        // Instantiate label
        labelInstance = Instantiate(PartNamelabel);
        labelInstance.transform.SetParent(transform);

        // Set text
        labelInstance.GetComponentInChildren<TextMeshProUGUI>().text = partID;

        // Position above object based on mesh bounds
        PositionLabel();

        labelInstance.SetActive(false);
    }

    void PositionLabel()
    {
        if (partRenderer != null)
        {
            Bounds bounds = partRenderer.bounds;
            Vector3 topPosition = bounds.center + new Vector3(0, bounds.extents.y + 0.1f, 0);
            labelInstance.transform.position = topPosition;
        }
    }


    void Update()
    {
        if (labelInstance != null && labelInstance.activeSelf)
        {
            // Billboard effect: always face the camera
            labelInstance.transform.rotation = Quaternion.LookRotation(mainCamera.transform.position - labelInstance.transform.position);
        }
    }

    public void OnPointerEnter()
    {
        PositionLabel(); // Recalculate in case object moved or scaled
        labelInstance.SetActive(true);
    }

    public void OnPointerExit()
    {
        labelInstance.SetActive(false);
    }


    //on collision with ground reset the part to its start position and rotation
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
           // transform.position = startPos;
            //transform.eulerAngles = startRot;
        }
    }

}