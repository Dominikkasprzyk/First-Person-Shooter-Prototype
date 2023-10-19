using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToSpawn;
    public void Spawn()
    {
        foreach(GameObject objectToSpawn in objectsToSpawn)
        {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        }  
    }
}
