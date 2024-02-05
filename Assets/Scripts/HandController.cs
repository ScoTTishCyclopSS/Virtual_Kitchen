using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using XRController = UnityEngine.InputSystem.XR.XRController;

public class HandController : MonoBehaviour
{
    private XRDirectInteractor _xrDirectInteractor;
    public Animator handAnimator;

    void Start()
    {
        _xrDirectInteractor = GetComponent<XRDirectInteractor>();
    }

    public void UpdateAnimation(bool b)
    {
        GameObject item = _xrDirectInteractor.firstInteractableSelected.transform.gameObject;
        XRCustomInteractable xrCustomInteractable = item.GetComponent<XRCustomInteractable>();
        handAnimator.SetBool(xrCustomInteractable.animationLayerTag, b);
    }
}

