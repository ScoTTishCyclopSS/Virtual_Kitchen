using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRCustomInteractable : XRGrabInteractable
{
    public string animationLayerTag = string.Empty;
    public Transform leftAttachTransform;
    private Transform pivot;
    private Transform pivotL;
    
    
    private void Start()
    {
        pivot = attachTransform;
        pivotL = leftAttachTransform;
    }
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.GetType() == typeof(XRDirectInteractor))
        {
            Transform handTransform = args.interactorObject.transform;
            
            if (handTransform.CompareTag("LeftHand"))
            {
                attachTransform = pivotL;
                //attachTransform.SetPositionAndRotation(handTransform.position, handTransform.rotation);
            }
            
            if (handTransform.CompareTag("RightHand"))
            {
                attachTransform = pivot;
            }
        }

        else if (args.interactorObject.GetType() == typeof(XRCustomSocket))
        {
            attachTransform = transform;
        }

        base.OnSelectEntered(args);
    }

}
