using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CookingPot : MonoBehaviour
{
    
    
    public XRGrabInteractable grabInteractable;
    public InteractionLayerMask maskWhileCooking;
    public int minIngrToStart = 2;
    public int capacity = 12;
    public float TimeToCook = 20f;
    public float TimeToAdd = 10f;
    public GameObject fluid;

    [SerializeField]private float _currTime;
    [SerializeField]private bool isCooking;
    private InteractionLayerMask _maskStandard;
    private bool onBurner;
    
    private Dictionary<string, Ingredient> _currentIngredients;
    private XRSocketInteractor socketPot;
    public PourDetector _pourDetector;

    public AudioClip boiling;
    public AudioClip ready;
    private AudioSource _sound;

    void Start()
    {
        onBurner = false;
        _currTime = TimeToCook;
        _currentIngredients = new Dictionary<string, Ingredient>();
        socketPot = GetComponent<XRSocketInteractor>();
        _maskStandard = grabInteractable.interactionLayers;
        _sound = GetComponent<AudioSource>();

    }
    
    void Update()
    {
        if (isCooking && _currentIngredients.Count >= minIngrToStart)
        {
            if (_currTime > 0)
            {
                _currTime -= Time.deltaTime;
                ChangeLayerMask(maskWhileCooking);
            }
            else
            {
                _currTime = 0;
                ChangeLayerMask(_maskStandard);
                
                if (capacity == 1 && isFull())
                {   
                    // pan exception
                    ChangeLayerMask(maskWhileCooking);
                }
            }
        }
    }

    // onSelectionEntered
    public void CheckIngredient()
    {
        if (!isFull())
        {
            GameObject objectSelected = socketPot.GetOldestInteractableSelected().transform.gameObject;
            Ingredient ingredient = objectSelected.GetComponent<Ingredient>();
            
            _currentIngredients[ingredient.Name] = ingredient;
            if (_currentIngredients.Count == minIngrToStart && onBurner)
            {
                _sound.clip = boiling;
                _sound.loop = true;
                _sound.Play();
            } 
            _currTime += TimeToAdd;

            if (capacity > 1)
            {
                Destroy(objectSelected);
            }
            SetCooking(true);
        }
    }
    
    public void ChangeLayerMask(InteractionLayerMask layerMask)
    {
        grabInteractable.interactionLayers = layerMask;
    }

    public List<Ingredient> getIngredients()
    {
        return _currentIngredients.Values.ToList();
    }
    
    public void SetCooking(bool b)
    {
        if (onBurner)
        {
            isCooking = b;
            
            fluid.SetActive(true);
            _pourDetector.SetActivePour(true);

        }
            
    }

    public void SetOnBurner()
    {
        onBurner = true;
    }
    
    public void RemoveFromBurner()
    {
        onBurner = false;
        _sound.Pause();
    }

    public void cleanPot()
    {
        _currentIngredients.Clear();
        fluid.SetActive(false);
        _pourDetector.SetActivePour(false);
        _pourDetector.DeleteSteam();
        _pourDetector.enabled = false;
    }

    public bool isEmpty()
    {
        return _currentIngredients.Count <= 0;
    }

    private bool isFull()
    {
        return _currentIngredients.Count == capacity;
    }
}
