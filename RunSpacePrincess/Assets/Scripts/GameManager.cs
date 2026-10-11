using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
	
	[SerializeField] public GameObject gameOverPanel;
	[SerializeField] public GameObject winPanel;
	[SerializeField] public GameObject tutorialPanel;
    [SerializeField] public Slider progressBar;
    [SerializeField] public GameObject princess;
    [SerializeField] public Transform despawnZone;
	[SerializeField] public Spawner spawner;
	[SerializeField] public float maxTime = 100f;
	[SerializeField] public Animator floorAnimator;
	[SerializeField] public AudioSource audioSource;
	[SerializeField] private float fadeDuration = 3f;
	

	public int difficulty = 1;
	private float difficultyTimer = 0;
	private float diffTimerLimit = 5f;

	private float gameTime = 0;
	private float lastSpawnTime = 1f;
	public float spawnRate = 1.5f;
	public bool isPaused = true;
	public bool winTriggered = false;

	public bool tutorialOn = true;
	

    void Awake()
    {
        instance = this;
    }

	void Update ()
	{
		if(gameTime >= maxTime && !winTriggered)
		{
			winTriggered = true;
            PauseGame(true);
			princess.GetComponentInChildren<Animator>().SetTrigger("Win");
		}
		

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

	public void TurnOffTutorial()
	{
		tutorialPanel.SetActive(false);
		PauseGame(false);
		tutorialOn = false;
		audioSource.Play();
	}

	public void PauseGame(bool value = true)
	{
		isPaused = value;
		floorAnimator.SetFloat("idleSpeed", value ? 0f : 1f);
		
	}
	public void GameOver()
	{
		PauseGame(true);
		FadeMusic();
		SoundManager.PlaySound(SoundManager.SoundType.Defeat);
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

	public void Win()
	{
		FadeMusic();
		SoundManager.PlaySound(SoundManager.SoundType.Victory);
		winPanel.SetActive(true);
	}

	public void FadeMusic()
	{
		StartCoroutine(FadeOut());
	}


    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            audioSource.volume = Mathf.Lerp(
                startVolume,
                0f,
                time / fadeDuration
            );

            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }



}
