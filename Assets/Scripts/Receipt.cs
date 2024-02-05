using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Receipt : MonoBehaviour
{

    public Sprite Image => myReceipt.image;
    public string Name => myReceipt.name;
    public List<Ingredient> Ingredients => myReceipt.ingredients;
    
    [Serializable]
    public struct MyReceipt
    {
        public Sprite image;
        public string name;
        public List<Ingredient> ingredients;
    }

    public MyReceipt myReceipt;

    public override string ToString()
    {
        string ret = "";

        foreach (var i in Ingredients)
        {
            ret += "• " + i.Name + "\n";
        }

        return ret;
    }
}
