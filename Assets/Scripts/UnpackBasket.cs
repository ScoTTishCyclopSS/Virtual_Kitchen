using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UnpackBasket : MonoBehaviour
{

    public GameObject foodSockets; // parent
    public XRCustomSocket xrCustomSocket;

    private List<Transform> _sockets;
    private Basket _basket;

    public void UnpackBasketIngredients()
    {
        _sockets = GetAllSockets(foodSockets);
        Debug.Log(_sockets.Count);

        Transform basketTransform = GetBasketInteractable();
        _basket = basketTransform.GetComponent<Basket>();
        // xrSelectInteractable.transform.GetComponent<Basket>();
        _basket.UnpackBasket(_sockets);
        // todo: destroy
    }
    
    private List<Transform> GetAllSockets(GameObject foodSockets)
    {
        List<Transform> ret = new List<Transform>();
        foreach (Transform child in foodSockets.transform)
            ret.Add(child);
        return ret;
    }

    private Transform GetBasketInteractable()
    {
        IXRSelectInteractable xrSelectInteractable = xrCustomSocket.GetOldestInteractableSelected();
        Debug.Log(xrCustomSocket.targetTag);
        return xrSelectInteractable.transform;
    }
}
