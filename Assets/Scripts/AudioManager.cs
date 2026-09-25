using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource music;
    public AudioSource sfx;

    [Header("Clips")]
    public AudioClip backgroundMusic;
    public AudioClip witchLaughSound;
    public AudioClip thudSound;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PlayMusic();
        PlaySFX(witchLaughSound);
    }

    public void PlayMusic()
    {
        music.clip = backgroundMusic;
        music.loop = true;
        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public void RestartMusic()
    {
        music.Stop();
        music.Play();
    }

    public void PlayThud()
    {
        PlaySFX(thudSound);
    }

    void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfx.PlayOneShot(clip);
    }
}