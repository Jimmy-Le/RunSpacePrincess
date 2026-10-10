using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public Transform princessTransform;
    [SerializeField] public bool isX;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        Vector3 newPosition = transform.position;
        if (isX)
        {
            newPosition.x = princessTransform.position.x;
        }
        else
        {
            newPosition.y = princessTransform.position.y;
        }
        this.transform.position =  newPosition;
    }
}
