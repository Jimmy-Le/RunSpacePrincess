using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public GameObject princess;
    [SerializeField] public Transform despawnZone;
	[SerializeField] public Spawner spawner;

	[SerializeField] public float maxTime = 100f;
	private float gameTime = 0;
	private float lastSpawnTime = 0;
	public float spawnRate = 5f;

    void Awake()
    {
        instance = this;
    }

	void Update ()
	{
		gameTime += Time.deltaTime;
 		lastSpawnTime += Time.deltaTime;


		if(lastSpawnTime >= spawnRate && gameTime <= maxTime)
		{
			spawner.spawnRandom();
   			lastSpawnTime = 0;
		}	
		
	}

}
