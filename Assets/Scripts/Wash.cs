using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wash : MonoBehaviour
{
    public Transform teleportPosition;

    private void OnTriggerEnter(Collider other)
    {
        Transform objTeleport = other.gameObject.transform;
        objTeleport.position = teleportPosition.position;
        objTeleport.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
