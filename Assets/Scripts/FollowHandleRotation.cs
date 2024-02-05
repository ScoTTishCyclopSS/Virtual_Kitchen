using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandleRotation : MonoBehaviour
{
    public GameObject doorHandleGrab;
    
    void Update()
    {
        float x = doorHandleGrab.transform.localRotation.x;
        float y = transform.localRotation.y;
        float z = transform.localRotation.z;
        transform.localRotation = Quaternion.Euler(x, y, z);
    }
}
