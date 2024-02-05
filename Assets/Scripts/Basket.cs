using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public List<GameObject> ingredients;

    public void UnpackBasket(List<Transform> _sockets)
    {
        for (int j = 0; j < ingredients.Count; j++)
        {
            Vector3 posToUnpack = _sockets[j].position;
            Instantiate(ingredients[j], posToUnpack, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
