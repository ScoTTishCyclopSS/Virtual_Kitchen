using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IvyGenerator : MonoBehaviour
{
    public GameObject leaf;
    public float leafScaleMin = 0.1f;
    public float leafScaleMax = 0.1f;
    public int spawnCount = 20;
    public Vector3 randUserRotMin;
    public Vector3 randUserRotMax;

    public void SpawnLeafs()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Debug.LogError("Can execute only in edit mode");
            return;
        }

        if (!leaf)
        {
            Debug.LogError("No leaf gameObject!");
            return;
        }
        
        Renderer objRenderer = leaf.GetComponent<Renderer>();
        if (!objRenderer)
        {
            Debug.LogError("No render component in leaf gameObject!");
            return;
        }

        DestroyAllLeafs();
        
        // Vector3 target = leaf.transform.position;
        // Vector3 planeNormalProjection = Vector3.ProjectOnPlane(target, transform.up);
        // // translation along the normal vector
        // planeNormalProjection += + Vector3.Dot(transform.position, transform.up) * transform.up;
        //
        // leaf.transform.rotation = transform.rotation;
        // leaf.transform.position = planeNormalProjection;
        // Debug.DrawLine(target, planeNormalProjection, Color.red, 30, false);

        Collider collider = GetComponent<Collider>();

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randPos = GenerateRandomPoint(collider);
            Quaternion randRot = GenerateRandomRotation();
            float randScale = GenerateRandomScale();
            
            GameObject newLeaf = Instantiate(leaf, randPos, randRot, transform);
            newLeaf.transform.localScale *= randScale;
        }
    }

    private float GenerateRandomScale()
    {
        return Random.Range(leafScaleMin, leafScaleMax);
    }

    private Quaternion GenerateRandomRotation()
    {
        float randX = Random.Range(randUserRotMin.x, randUserRotMax.x);
        float randY = Random.Range(randUserRotMin.y, randUserRotMax.y);
        float randZ = Random.Range(randUserRotMin.z, randUserRotMax.z);
        
        // no gimbal lock, heheheheh
        Quaternion result = Quaternion.identity;
        result *= Quaternion.AngleAxis(randZ, transform.forward);
        result *= Quaternion.AngleAxis(randX, transform.right);
        result *= Quaternion.AngleAxis(randY, transform.up);

        return transform.rotation * result;
    }

    private Vector3 GenerateRandomPoint(Collider collider)
    {
        float randX = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float randY = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        float randZ = Random.Range(collider.bounds.min.z, collider.bounds.max.z);
        
        Vector3 result = new Vector3(randX, randY, randZ);
        if (!IsInside(result, collider))
        {
            result = GenerateRandomPoint(collider);
        }
        return result;
    }
    
    public bool IsInside(Vector3 point, Collider collider)
    {
        return collider.ClosestPoint(point) == point;
    }
    
    public void DestroyAllLeafs()
    {
        if (!leaf.GetComponent<Renderer>())
        {
            Debug.LogError("No render component in leaf gameObject!");
            return;
        }

        foreach (Transform t in transform)
        {
            EditorApplication.delayCall += () => { DestroyImmediate(t.gameObject); };
        }
    }
}
