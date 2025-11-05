using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapAblePartHandler : MonoBehaviour
{
    public string snapTagId = "";
    public int snapIndex = -1;
    public bool isSnapped = false;
    private GameObject snapTarget;
    public Vector3 startPos;
    public Vector3 startRot;
    [SerializeField]
    private MeshRenderer meshrenderer;
    [SerializeField]
    private Material originalmat;
    [SerializeField]
    private Material transparentHighlightMat; 
    public int mainBodyMaterialIndex = 0;
    private Rigidbody _rigidbody;
    public BoxCollider _boxCollider;
    private XRGrabInteractable _grabInteractable;

    public event Action<SnapAblePartHandler> OnSnapped;


    private void Awake()
    {
        snapTagId = transform.GetComponent<PartAttribute>().partID;
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
        snapTarget =transform.gameObject;
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.enabled = false; // Disable grabbing initially
        GetMeshrenderer();
    }

    private void Start()
    {
      
    }

    public void Initialize()
    {
        Debug.Log("SnapAblePartHandler Initialize called for " + meshrenderer.materials.Length);
        //if meshrenderer have multiple materials then assign transparentHighlightMat to mainBodyMaterialIndex
        if (meshrenderer.materials.Length > 0)
        {
            Debug.Log("SnapAblePartHandler Initialize called for multiple materials ");
            var mats = meshrenderer.materials; 
            mats[mainBodyMaterialIndex] = transparentHighlightMat; 
            meshrenderer.materials = mats;
        }
        else
        {
            Debug.Log("SnapAblePartHandler Initialize called for single material ");
            meshrenderer.material = transparentHighlightMat;
        }
        _boxCollider.isTrigger = true;

    }

    private void GetMeshrenderer()
    {
        //find meshrenderer if not assigned then look in children with same snapTagId name having MeshRenderer component, there could be multiple children with same name so find that one having MeshRenderer component attached
        if (meshrenderer == null)
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.name == snapTagId)
                {
                    meshrenderer = child.GetComponent<MeshRenderer>();
                    if (meshrenderer != null)
                    {
                        if (meshrenderer.materials.Length > 0)
                        {
                            var material = meshrenderer.materials;
                            originalmat = material[mainBodyMaterialIndex];
                        }
                        else
                        {
                            originalmat = meshrenderer.material;
                        }
                        break;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PartAttribute otherTag = other.transform.GetComponent<PartAttribute>() as PartAttribute;
        if ((otherTag.partID==snapTagId) && !isSnapped)
        {
            snapAnimation(other.transform);
        }
    }

    private void snapAnimation(Transform otherGM)
    {
        otherGM.GetComponent<XRGrabInteractable>().enabled = false; // Disable grabbing during snap
        otherGM.GetComponent<Rigidbody>().isKinematic = true;
        otherGM.GetComponent<Rigidbody>().useGravity = false;
        // Snap the part to the snap zone's position and rotation using Do Tween animation
        Sequence snapSequence = DOTween.Sequence();
        snapSequence.Append(otherGM.DOMove(snapTarget.transform.position, 0.5f).SetEase(Ease.InOutSine));
        snapSequence.Append(otherGM.DORotateQuaternion(Quaternion.Euler(snapTarget.transform.eulerAngles), 0.5f).SetEase(Ease.InOutSine));

        snapSequence.onComplete = () =>
        {
            // Disable physics to prevent further movement
            if (_rigidbody != null)
            {
                _rigidbody.isKinematic = true;
                _rigidbody.linearVelocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
            isSnapped = true;
            meshrenderer.material = originalmat;
            otherGM.gameObject.SetActive(false);
            //otherGM.transform.position =otherGM.GetComponent<PartAttribute>().startPos;
            //otherGM.transform.rotation = Quaternion.Euler(otherGM.GetComponent<PartAttribute>().startRot);
            //otherGM.GetComponent<XRGrabInteractable>().enabled = true;
            //otherGM.GetComponent<Rigidbody>().isKinematic = false;
            //otherGM.GetComponent<Rigidbody>().useGravity = true;
            //Debug.Log("Snapped other part: " + otherGM.gameObject.name);
            //Debug.Log("this part name :- " + this.gameObject.name);
            Destroy(otherGM.gameObject,1f);
            OnSnapped?.Invoke(this);
        };
    }

    //private void SnapToZone(Transform otherGM)
    //{
    //    // Snap the part to the snap zone's position and rotation using Do Tween animation
    //    Sequence snapSequence = DOTween.Sequence();
    //    snapSequence.Append(otherGM.DOMove(snapTarget.transform.position, 0.5f).SetEase(Ease.InOutSine));
    //    snapSequence.Append(otherGM.DORotateQuaternion(Quaternion.Euler(snapTargetRot), 0.5f).SetEase(Ease.InOutSine));

    //   // otherGM.position = snapTarget.transform.position;
    //    //otherGM.rotation = Quaternion.Euler(snapTargetRot);
    //    // Disable physics to prevent further movement
    //    if (_rigidbody != null)
    //    {
    //        _rigidbody.isKinematic = true;
    //        _rigidbody.linearVelocity = Vector3.zero;
    //        _rigidbody.angularVelocity = Vector3.zero;
    //    }
    //    // Optionally, you can parent the part to the snap zone for better organization
    //    otherGM.SetParent(snapTarget.transform);
    //    isSnapped = true;
    //    meshrenderer.material = originalmat;
    //}


}
