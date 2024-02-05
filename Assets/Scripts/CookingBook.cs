using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookingBook : MonoBehaviour
{
    public List<Receipt> receipts;
    [Range(0.0f, 1.0f)]
    public float koef = 0.1f;

    private Receipt _currentReceipt;

    private void Start()
    {
        _currentReceipt = receipts[0];
    }

    public Receipt GetCurrentReceipt()
    {
        return _currentReceipt;
    }

    public void UpdateCurrentReceipt(bool right)
    {
        int index = receipts.IndexOf(_currentReceipt);
        
        Debug.Log(right);

        if (right && index + 1 < receipts.Count)
        {
            _currentReceipt = receipts[index + 1];
        }
        else if (index - 1 >= 0)
        {
            _currentReceipt = receipts[index - 1];
        }
    }

    public string RateMySoup(List<Ingredient> ingredients)
    {
        string result = "";
        float bestRating = 0f;
        foreach (var receipt in receipts)
        {
            int recIngr = receipt.Ingredients.Count();
            float sameIngr = ingredients.Where(x => receipt.Ingredients.Any(y => y.Name.Equals(x.Name))).Count();
            float wrongIngr = ingredients.Count() - sameIngr;

            float baseRating = (sameIngr / recIngr) * 100f;
            float penalty = (wrongIngr / recIngr) * 100f;
            float finalRating = baseRating - (penalty * koef);
            
            //Debug.Log("Receipt count: " + recIngr + " | Wrong: " + wrongIngr + " | Same: " + sameIngr + " | Base rating: " + baseRating + " | Penalty: " + penalty);
            if (finalRating > bestRating)
            {
                bestRating = finalRating;
                result = receipt.Name + ": " + Mathf.RoundToInt(finalRating) + "%";
            }
        }

        return result;
    }
}
