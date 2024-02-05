using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TagTrigger : MonoBehaviour
{
    public string tagToCompare;
    private XRSocketInteractor _interactor;

    private void Start()
    {
        _interactor = GetComponent<XRSocketInteractor>();
    }

    private void OnTriggerStay(Collider other)
    {
        
        _interactor.socketActive = (other.gameObject.tag == tagToCompare);
        //if (!_interactor.hasSelection)
        //{
        //    _interactor.socketActive = (other.gameObject.tag == tagToCompare);
        //}



    }
}
