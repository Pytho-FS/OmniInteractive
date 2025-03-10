using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip mainMenuMusic;

    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip playerJumpSFX;

    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private float sfxVolume = 1f;

    [SerializeField] private float volumeExponent = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ApplyMusicVolume();
            ApplySFXVolume();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayMainMenuMusic()
    {
        if (musicSource != null && mainMenuMusic != null) 
        {
            if (!musicSource.isPlaying)
            {
                musicSource.clip = mainMenuMusic;
                musicSource.loop = true;
                musicSource.volume = musicVolume;
                musicSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Music not assigned.");
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip, sfxVolume);
        }
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSFX);
    }

    public void PlayPlayerJump()
    {
        PlaySFX(playerJumpSFX);
    }

    public void SetMusicVolume(float newVolume)
    {
        musicVolume = Mathf.Clamp01(newVolume);
        ApplyMusicVolume();
    }

    public void SetSFXVolume(float newVolume)
    {
        sfxVolume = Mathf.Clamp01(newVolume);
        ApplySFXVolume();
    }

    private void ApplyMusicVolume()
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Pow(musicVolume, volumeExponent);
        }
    }

    private void ApplySFXVolume()
    {
        if (sfxSource != null)
        {
            sfxSource.volume = Mathf.Pow(sfxVolume, volumeExponent);
        }
    }

    private float sfxVolumeAdjusted()
    {
        return Mathf.Pow(sfxVolume, volumeExponent);
    }
}
