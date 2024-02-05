using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGrab : MonoBehaviour
{

    public GameObject doorHandleGrab;
    private Rigidbody _rigidbody;
    private Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPos = doorHandleGrab.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.MovePosition(doorHandleGrab.transform.position);
    }

    public void ResetPos()
    {
        Debug.Log("Reset handle pos: " + _startPos);
        doorHandleGrab.transform.localPosition = _startPos;
        doorHandleGrab.transform.localRotation = Quaternion.identity;
    }
}
