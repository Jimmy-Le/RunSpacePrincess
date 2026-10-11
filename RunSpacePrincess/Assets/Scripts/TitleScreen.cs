using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private float fadeDuration = 2f;


    private IEnumerator PlaySoundThenChangeScene()
    {
        SoundManager.PlaySound(SoundManager.SoundType.Click);

        // Wait until the sound has finished.
        while (SoundManager.instance.audioSource.isPlaying)
        {
            yield return null;
        }
		yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game");
    }


    public void StartGame()
    {
		FadeMusic();
		StartCoroutine(PlaySoundThenChangeScene());
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
