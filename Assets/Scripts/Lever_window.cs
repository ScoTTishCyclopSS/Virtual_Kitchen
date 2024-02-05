using System;
using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Lever_window : MonoBehaviour
{
    public Transform window;
    public Vector3 offset;
    public float minWindSpeed = 0.1f;
    public float maxWindSpeed = 1f;
    public Material leafMaterial;
    public float duration = 3f;
    private Vector3 startPos;
    private Vector3 endPos;

    private void Start()
    {
        startPos = window.position;
        endPos = startPos + offset;
        SetWind(minWindSpeed);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lever"))
        {
            StartCoroutine(MoveWindow(startPos, endPos, maxWindSpeed));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lever"))
        {
            StartCoroutine(MoveWindow(endPos, startPos, minWindSpeed));
        }
    }

    private IEnumerator MoveWindow(Vector3 from, Vector3 to, float wind)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            window.position = Vector3.Lerp(from, to, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        window.position = to;
        SetWind(wind);
    }

    private void SetWind(float value)
    {
        leafMaterial.SetFloat("_Wind", value);
    }
    
    private float GetWind()
    { 
        return leafMaterial.GetFloat("_Wind");
    }
}