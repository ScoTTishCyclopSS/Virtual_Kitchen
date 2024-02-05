using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour
{
    public CookingPot cookingPot;
    public float timeToHeat;
    [SerializeField]
    private float _currTime;
    public bool isOn;
    public Material burnerMaterial;
    
    void Start()
    {
        _currTime = timeToHeat;
    }
    
    void Update()
    {
        UpdateHeat();
        UpdateMaterial();
        cookingPot.SetCooking(_currTime <= 0);
    }


    private void UpdateMaterial()
    {
        burnerMaterial.SetFloat("_Heat", Mathf.Lerp(1f, 0f, _currTime/timeToHeat));
    }

    private void UpdateHeat()
    {
        if (isOn)
        {
            if (_currTime > 0)
            {
                _currTime -= Time.deltaTime;
            }
            else
            {
                _currTime = 0;
            }
        }
        else
        {
            if (_currTime <= timeToHeat)
            {
                _currTime += Time.deltaTime;
            }
        }
    }

    public void CheckButton()
    {
        isOn = !isOn;
    }
}
