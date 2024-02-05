using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluid : MonoBehaviour
{
    public GameObject liquid;

    public float sloshSpeed = 50f;
    public float difference = 30f;

    private void Update()
    {
        Slosh();
    }

    private void Slosh()
    {
        Quaternion inverseRot = Quaternion.Inverse(transform.localRotation);

        Vector3 rotateTo = Quaternion
            .RotateTowards(liquid.transform.localRotation, inverseRot, sloshSpeed * Time.deltaTime).eulerAngles;
        
        // clamp
        rotateTo.x = ClampRotation(rotateTo.x);
        rotateTo.z = ClampRotation(rotateTo.z);

        liquid.transform.localEulerAngles = rotateTo;
    }

    private float ClampRotation(float v)
    {
        float ret;

        if (v > 180)
        {
            ret = Math.Clamp(v, 360 - difference, 360);
        }
        else
        {
            ret = Math.Clamp(v, 0, difference);
        }

        return ret;
    }
}
