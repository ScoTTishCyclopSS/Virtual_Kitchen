using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer _lineRenderer = null;
    private Vector3 targetPosition = Vector3.zero;
    public float MaxRayDistance = 2.0f;
    public float streamSpeed = 1.5f;
    private ParticleSystem splashParticleSystem = null;
    private Coroutine pourCoroutine = null;
    private CookingPot _cookingPot;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        splashParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        SetPos(0, transform.position);
        SetPos(1, transform.position);
    }

    public void GivePot(CookingPot cookingPot)
    {
        _cookingPot = cookingPot;
    }
    
    public void Begin()
    {
        StartCoroutine(UpdateParticle());
        pourCoroutine = StartCoroutine(BeginPour());
    }

    public void End()
    {
        StopCoroutine(pourCoroutine);
        pourCoroutine = StartCoroutine(EndPour());
    }
    
    private IEnumerator BeginPour()
    {
        while (gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();
            SetPos(0, transform.position);
            MoveToPosition(1, targetPosition);
            yield return null;
        }
    }

    private IEnumerator EndPour()
    {
        while (!OnPosition(0, targetPosition))
        {
            MoveToPosition(0, targetPosition);
            MoveToPosition(1, targetPosition);
            yield return null;
        }
        
        Destroy(gameObject);
    }
    

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, MaxRayDistance);
        Vector3 result = hit.collider ? hit.point : ray.GetPoint(MaxRayDistance);

        if (hit.collider.CompareTag("Bowl"))
        {
            Bowl b = hit.transform.gameObject.GetComponent<Bowl>();
            b.FillWithSoup(_cookingPot);
        }
        return result;
    }

    private bool OnPosition(int index, Vector3 targetPos)
    {
        Vector3 currentPoint = _lineRenderer.GetPosition(index);
        return currentPoint == targetPos;
    }

    private void MoveToPosition(int index, Vector3 targetPos)
    {
        Vector3 currentPoint = _lineRenderer.GetPosition(index);
        Vector3 newPos = Vector3.MoveTowards(currentPoint, targetPos, Time.deltaTime * streamSpeed);
        _lineRenderer.SetPosition(index, newPos);
    }

    private void SetPos(int index, Vector3 targetPos)
    {
        _lineRenderer.SetPosition(index, targetPos);
    }
    
    private IEnumerator UpdateParticle()
    {
        while (gameObject.activeSelf)
        {
            splashParticleSystem.gameObject.transform.position = targetPosition;
            bool isHit = OnPosition(1, targetPosition);
            splashParticleSystem.gameObject.SetActive(isHit);
            yield return null;
        }
    }
}
