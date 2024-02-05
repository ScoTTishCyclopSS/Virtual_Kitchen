using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourTreshold = 45;
    public Transform origin;
    public GameObject streamPrefab = null;

    public bool isPouring;
    private Stream currentStream = null;
    public CookingPot cookingPot;
    [SerializeField]private bool _isActive;

    public AudioSource source;
    public AudioClip pourSound;
    
    private void Update()
    {
        bool pourCheck = CheckPourAngle() < pourTreshold;
        source.clip = pourSound;

        if (isPouring != pourCheck && _isActive)
        {
            isPouring = pourCheck;
            if (isPouring)
            {
                StartPour();
            }
            else
            {
                if (_isActive)
                {
                    EndPour();
                    source.Stop();
                }
            }
        }

    }
    
    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
        source.Play();
    }

    private void EndPour()
    {
       currentStream.End();
       currentStream = null;
    }

    private float CheckPourAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        Stream str = streamObject.GetComponent<Stream>();
        str.GivePot(cookingPot);
        return str;
    }

    public void SetActivePour(bool a)
    {
        _isActive = a;
    }
    
    public void DeleteSteam()
    {
        //EndPour();
    }

}
