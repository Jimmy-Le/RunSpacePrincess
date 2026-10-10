using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
	
	[SerializeField] public GameObject gameOverPanel;
    [SerializeField] public Slider progressBar;
    [SerializeField] public GameObject princess;
    [SerializeField] public Transform despawnZone;
	[SerializeField] public Spawner spawner;
	[SerializeField] public float maxTime = 100f;

	public int difficulty = 1;
	private float difficultyTimer = 0;
	private float diffTimerLimit = 10f;

	private float gameTime = 0;
	private float lastSpawnTime = 1f;
	public float spawnRate = 1.5f;
	public bool isPaused = false;
	

    void Awake()
    {
        instance = this;
    }

	void Update ()
	{
		if (!isPaused)
		{
			gameTime += Time.deltaTime;
			lastSpawnTime += Time.deltaTime;
			difficultyTimer += Time.deltaTime;
			progressBar.value  = gameTime / maxTime;
			
			if (difficultyTimer > diffTimerLimit)
			{
				difficulty++;
				difficultyTimer = 0;
			}

		}
		
		if(lastSpawnTime >= spawnRate && gameTime <= maxTime)
		{
			spawner.spawnRandom();
   			lastSpawnTime = 0;
		}	
		
		
		
	}

	public void PauseGame(bool value = true)
	{
		isPaused = value;
		
	}
	public void GameOver()
	{
		PauseGame(true);
		gameOverPanel.SetActive(true);
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ToTitleScreen()
	{
		SceneManager.LoadScene("TitleScreen");
	}

}
