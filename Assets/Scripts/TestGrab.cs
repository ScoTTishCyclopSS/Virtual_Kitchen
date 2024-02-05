using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestGrab : MonoBehaviour
{
    public XRCustomSocket foodSocket;
    private XRGrabInteractable _xrGrabInteractable;

    private void Start()
    {
        _xrGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        foodSocket.socketActive = EnableFoodSocket();
    }

    private bool EnableFoodSocket()
    {
        IXRSelectInteractor xrSelectInteractor = _xrGrabInteractable.GetOldestInteractorSelecting();
        if (xrSelectInteractor != null)
        {
            if (xrSelectInteractor.transform.CompareTag("SocketBoard"))
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeLayerMask(string layerMask)
    {
        _xrGrabInteractable.interactionLayers = InteractionLayerMask.GetMask(layerMask);
    }
}
