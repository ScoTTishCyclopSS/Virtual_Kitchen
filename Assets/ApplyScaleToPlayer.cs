using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyScaleToPlayer : MonoBehaviour
{
    public Transform player;

    public void ApplyScale(float scale)
    {
        player.localScale = new Vector3(scale, scale, scale);
    }
}
