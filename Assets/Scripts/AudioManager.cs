using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip backgroundMusic;
    public AudioClip gameOverMusic;
    public AudioClip witchLaughSound;
    public AudioClip dieSound;             // thud
    public AudioClip dieSound2;            // splat

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PlayBackgroundMusic();
        PlaySFX(witchLaughSound);
    }

    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayGameOverMusic()
    {
        musicSource.clip = gameOverMusic;
        musicSource.loop = false; // false means it plays once
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void RestartMusic()
    {
        musicSource.Stop();
        musicSource.Play();
    }

    public void PlayDieSound()
    {
        float RandomGeneratedNumber = Random.Range(0, 100);

        AudioClip chosenDeathSound;

        // 50/50 chance of thud or splat
        if (RandomGeneratedNumber < 50)
        {
            chosenDeathSound = dieSound;
        }
        else
        {
            chosenDeathSound = dieSound2;
        }

        PlaySFX(chosenDeathSound);
    }

    void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
