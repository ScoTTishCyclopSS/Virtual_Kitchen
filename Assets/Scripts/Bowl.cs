using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    // Start is called before the first frame update
    public CookingBook book;
    private TextMeshProUGUI _textMeshProUGUI;
    public GameObject fluidObject;

    private void Start()
    {
        _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void FillWithSoup(CookingPot pot)
    {
        if (!pot.isEmpty())
        {
            _textMeshProUGUI.text = book.RateMySoup(pot.getIngredients());
            pot.cleanPot();
            fluidObject.SetActive(true);
        }
    }
}
