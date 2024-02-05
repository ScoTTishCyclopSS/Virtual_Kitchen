using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageUpdate : MonoBehaviour
{
    public CookingBook book;
    private Image _image;
    public TextMeshProUGUI titleBox;
    public TextMeshProUGUI receiptBox;
    
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        Receipt currRcpt = book.GetCurrentReceipt();
        
        _image.sprite = currRcpt.Image;
        titleBox.text = currRcpt.Name;
        receiptBox.text = currRcpt.ToString();
    }
}
