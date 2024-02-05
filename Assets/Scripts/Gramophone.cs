using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gramophone : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    private XRCustomSocket _xrSocketInteractor;
    public float spinSpeed = 0.3f;
    private Coroutine _currCoroutine = null;

    private void Start()
    {
        _xrSocketInteractor = GetComponent<XRCustomSocket>();
    }

    public void AssignSong()
    {
        IXRSelectInteractable record = _xrSocketInteractor.GetOldestInteractableSelected();
        GameObject recordObj = record.transform.gameObject;

        audioSource.clip = recordObj.GetComponent<Record>().clip;
    }
    
    public void SpinTheWheel()
    {
        
        _currCoroutine = StartCoroutine(RotateRecord());
    }
    
    public void StopTheWheel()
    {
        StopCoroutine(_currCoroutine);
    }

    private IEnumerator RotateRecord()
    {
        float elapsedTime = 0;
        while (gameObject.activeSelf)
        {
            elapsedTime += Time.deltaTime;
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
