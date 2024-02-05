using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBowl : MonoBehaviour
{
    public List<GameObject> bowlPrefabs;
    public Transform posToSpawn;
    public CookingBook Book;

    public void SpawnBowl()
    {
        GameObject bowlToSpawn = bowlPrefabs[Random.Range(0, bowlPrefabs.Count)];
        bowlToSpawn.GetComponent<Bowl>().book = Book;
        Instantiate(bowlToSpawn, posToSpawn.position, Quaternion.identity);
    }
}
