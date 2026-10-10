using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    [SerializeField] public float speed = 2f;
    // Update is called once per frame
    void Update()
    {
		if(GameManager.instance.isPaused)
		{
			return;
		}

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x <= GameManager.instance.despawnZone.position.x)
        {
            Destroy(this.gameObject);
        }
    }
    
    
    
}
