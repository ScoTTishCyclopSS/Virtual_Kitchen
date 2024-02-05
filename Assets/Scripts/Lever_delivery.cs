using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_delivery : MonoBehaviour
{
    public Transform door;
    public Vector3 offset;

    private Vector3 startPos;

    private void Start()
    {
        startPos = door.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Basket"))
        {
            StartCoroutine(OpenDoor());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Basket"))
        {
            StartCoroutine(CloseDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        float elapsedTime = 0;
        while (door.position != startPos + offset)
        {
            door.position = Vector3.Lerp(startPos, startPos + offset, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    private IEnumerator CloseDoor()
    {
        float elapsedTime = 0;
        while (door.position != startPos)
        {
            door.position = Vector3.Lerp(startPos + offset, startPos, elapsedTime);
            elapsedTime += Time.deltaTime * 0.2f;
            yield return null;
        }
    }
}
