using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] public List<GameObject> spawnObjects;
    [SerializeField] public Transform upperLimit;
    [SerializeField] public Transform lowerLimit;
    [SerializeField] public Transform folder;


    public void spawnRandom()
    {
        int random = Random.Range(0, spawnObjects.Count);
        float randomY = Random.Range(lowerLimit.position.y, upperLimit.position.y);
        Vector3 objectPosition = this.transform.position;
        objectPosition.y = randomY;

        GameObject spawnedObject = spawnObjects[random];
        
        Instantiate(spawnedObject, objectPosition, Quaternion.identity, folder);


    }


}
