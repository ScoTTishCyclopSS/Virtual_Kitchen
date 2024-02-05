using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ingredient : MonoBehaviour
{
    public string Name;
    public bool isWashable = true;
    public int chopsPerState = 3;
    public List<Mesh> meshes;

    public InteractionLayerMask maskWhileCooking;

    private int _currentChops;
    private BoxCollider[] _colliders; // 1st collider - for grab, 2nd - is trigger
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private bool _isWashed;
    private bool _isReady;

    private XRCustomInteractable _xrCustomInteractable;
    private InteractionLayerMask _maskStandard;

    private AudioSource _source;

    private void Start()
    {
        _colliders = GetComponents<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _isWashed = false;
        _isReady = false;
        _currentChops = chopsPerState;
        _xrCustomInteractable = GetComponent<XRCustomInteractable>();
        _maskStandard = _xrCustomInteractable.interactionLayers;
        _source = GetComponent<AudioSource>();

    }

    private void ChangeMesh(Mesh mesh)
    {
        _meshFilter.sharedMesh = mesh;
        _meshRenderer.ResetLocalBounds();
        Bounds bounds = _meshRenderer.localBounds;

        foreach (var c in _colliders)
        {
            c.center = new Vector3(0f, bounds.size.y / 2f, 0f);
            c.size = bounds.size;
        }
    }

    private int GetMeshIndex(Mesh mesh)
    {
        return meshes.FindIndex(m => m == mesh);
    }

    private Mesh GetCurrentMesh()
    {
        return _meshFilter.sharedMesh;
    }

    private void Wash()
    {
        if (isWashable && !_isWashed)
        {
            Debug.Log("wash!");
            ChangeMesh(meshes[1]);
            _isWashed = true;
        }
    }

    private void Chop()
    {
        bool readyToChop = (isWashable && _isWashed) || !isWashable;
        
        if (!_isReady && readyToChop)
        {
            if (_currentChops == 0)
            {
                Mesh nextMesh = meshes[GetMeshIndex(GetCurrentMesh()) + 1];
                ChangeMesh(nextMesh);
                _currentChops = chopsPerState;
            }
            else
            {
                _currentChops -= 1;
                _source.Play();
                _xrCustomInteractable.interactionLayers = maskWhileCooking;
            }
            
            // if our ingredient is in last stage -> is ready
            if (GetCurrentMesh() == meshes.Last())
            {
                _isReady = true;
                _xrCustomInteractable.interactionLayers = _maskStandard;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Wash();
        }

        if (other.gameObject.CompareTag("Knife"))
        {
            Chop();
        }
    }
}