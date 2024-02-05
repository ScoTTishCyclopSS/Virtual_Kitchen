using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour
{
    public UnityEvent onPressed, onRealised;

    public float threshold = 0.1f;
    public float deadZone = 0.025f; // to avoid bounciness

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;

    // Start is called before the first frame update
    void Start()
    {
        _isPressed = false;
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = GetValue();
        if (!_isPressed && threshold + value >= 1)
            Pressed();
        if (_isPressed && value - threshold <= 0)
            Released();
    }

    private float GetValue()
    {
        float value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;

        if (Math.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1.0f, 1.0f);
    }

    private void Pressed()
    {
        Debug.Log("Pressed");
        _isPressed = true;
        onPressed.Invoke();
        
    }

    private void Released()
    {
        _isPressed = false;
        onRealised.Invoke();
        Debug.Log("Released");

    }
}
