using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lever_basket : MonoBehaviour
{

    public Transform basketSpawn;
    public GameObject basket;
    
    private TextMeshProUGUI _textMeshPro;
    public GameObject text;

    public float timeReset = 90;
    public float timeRemaining = 10;
    private bool _timerIsRunning;

    private Collider _triggerCollider;

    public AudioSource source;
    public AudioClip clip;

    private void Start()
    {
        _triggerCollider = GetComponent<BoxCollider>();
        _triggerCollider.enabled = false;
        _timerIsRunning = true;
        _textMeshPro = text.GetComponent<TextMeshProUGUI>();
        timeRemaining = 10;
        source.clip = clip;
    }

    void Update()
    {
        if (_timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateCanvas();
            }
            else
            {
                timeRemaining = 0;
                _timerIsRunning = false;
                _triggerCollider.enabled = true;
                MsgCanvas("Ready!");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lever"))
        {
            source.Play();
        }
        
        if (other.gameObject.CompareTag("Lever") && !_timerIsRunning)
        {
            Debug.Log("Basket Spawn");
            Instantiate(basket, basketSpawn.position, Quaternion.identity);
            timeRemaining = timeReset;
            _timerIsRunning = true;
        }
    }

    private void UpdateCanvas()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        _textMeshPro.text = minutes + ":" + seconds;
    }

    private void MsgCanvas(string msg)
    {
        _textMeshPro.text = msg;
    }
}
