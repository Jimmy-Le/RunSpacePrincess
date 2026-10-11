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
	[SerializeField] public AudioSource backgroundAudioSource;
	[SerializeField] private float fadeDuration = 3f;
	

	public int difficulty = 1;
	private float difficultyTimer = 0;
	private float diffTimerLimit = 20f;

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
		FadeMusic(backgroundAudioSource);
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
		FadeMusic(audioSource);
		SoundManager.PlaySound(SoundManager.SoundType.Defeat, 0.3f);
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
		FadeMusic(audioSource);
		SoundManager.PlaySound(SoundManager.SoundType.Victory, 0.4f);
		winPanel.SetActive(true);
	}

	public void FadeMusic(AudioSource sound)
	{
		StartCoroutine(FadeOut(sound));
	}


    private IEnumerator FadeOut(AudioSource sound)
    {
        float startVolume = sound.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            sound.volume = Mathf.Lerp(
                startVolume,
                0f,
                time / fadeDuration
            );

            yield return null;
        }

        sound.volume = 0f;
        sound.Stop();
    }



}
