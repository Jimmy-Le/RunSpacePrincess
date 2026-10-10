using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] public List<GameObject> spawnObjects;
    [SerializeField] public Transform upperLimit;
    [SerializeField] public Transform lowerLimit;
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
