using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using UnityEngine.UI;

public class Page_turn : MonoBehaviour
{
    public AlembicStreamPlayer _bookPlayer;
    public CookingBook _book;
    public bool isRight;
    public float pageTurnTime;
    public Material soupIconMat;
    public TextMeshProUGUI titleBox;
    public TextMeshProUGUI receiptBox;
    
    private Button _btn;
    
    private void Start()
    {
        _btn = GetComponent<Button>();
    }

    public void TurnPage()
    {
        _btn.enabled = false;
        _bookPlayer.CurrentTime = isRight ? _bookPlayer.StartTime : _bookPlayer.EndTime;
        _book.UpdateCurrentReceipt(isRight);
        StartCoroutine(TurnPageEvent());
    }

    private IEnumerator TurnPageEvent()
    {
        float elapsedTime = 0;

        if (isRight)
        {
            while (_bookPlayer.CurrentTime < _bookPlayer.EndTime - 0.05f)
            {
                _bookPlayer.CurrentTime = Mathf.Lerp(_bookPlayer.StartTime, _bookPlayer.EndTime, elapsedTime/pageTurnTime);
                float norm = Mathf.Lerp(0f, 1f, elapsedTime / pageTurnTime);
                soupIconMat.SetFloat("_Opacity", norm);
                titleBox.color = new Color(titleBox.color.r, titleBox.color.g, titleBox.color.b, norm);
                receiptBox.color = new Color(receiptBox.color.r, receiptBox.color.g, receiptBox.color.b, norm);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (_bookPlayer.CurrentTime > 0.05f)
            {
                _bookPlayer.CurrentTime = Mathf.Lerp(_bookPlayer.EndTime, _bookPlayer.StartTime, elapsedTime/pageTurnTime);
                float norm = Mathf.Lerp(0f, 1f, elapsedTime / pageTurnTime);
                soupIconMat.SetFloat("_Opacity", norm);
                titleBox.color = new Color(titleBox.color.r, titleBox.color.g, titleBox.color.b, norm);
                receiptBox.color = new Color(receiptBox.color.r, receiptBox.color.g, receiptBox.color.b, norm);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        ResetAnim();
    }

    private void ResetAnim()
    {
        _bookPlayer.CurrentTime = 0f;
        _btn.enabled = true;
    }
}