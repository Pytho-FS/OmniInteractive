using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip ninjaMusic;
    [SerializeField] private AudioClip jungleMusic;
    [SerializeField] private AudioClip fallingMusic;
    [SerializeField] private AudioClip creditsMusic;


    [SerializeField] private AudioClip buttonClickSFX;
    [SerializeField] private AudioClip playerJumpSFX;
    [SerializeField] private AudioClip crystalDropSFX;
    [SerializeField] private AudioClip crystalPickupSFX;
    [SerializeField] private AudioClip portalSoundSFX;
    [SerializeField] private AudioClip windSoundSFX;

    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private float sfxVolume = 1f;

    [SerializeField] private float volumeExponent = 2f;

    private float jumpPitchMin = 0.095f;
    private float jumpPitchMax = 1.05f;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusicForScene(scene.name);
    }

    public void PlayBackgroundMusicForScene(string sceneName)
    {
        if (musicSource == null)
        {
            Debug.LogWarning("Music source not assigned.");
            return;
        }

        StopMusic();

        switch (sceneName)
        {
            case "Main Menu":
                {
                    musicSource.clip = mainMenuMusic;
                    break;
                }
            case "Minigame_0":
                {
                    musicSource.clip = ninjaMusic;
                    break;
                }
            case "Minigame_1":
                {
                    musicSource.clip = jungleMusic;
                    break;
                }
            case "Minigame_2":
                {
                    musicSource.clip = fallingMusic;
                    break;
                }
            case "Credits":
                {
                    musicSource.clip = creditsMusic;
                    break;
                }
            default:
                musicSource.clip = mainMenuMusic;
                break;
        }

        if (musicSource.clip != null)
        {
            musicSource.loop = true;
            ApplyMusicVolume();
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("No music clip assigned for scene: " + sceneName);
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
            sfxSource.PlayOneShot(clip, sfxVolumeAdjusted());
        }
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSFX);
    }

    public void PlayCrystalPickup()
    {
        PlaySFX(crystalPickupSFX);
    }

    public void DropCrystalSkull()
    {
        PlaySFX(crystalDropSFX);
    }

    public void PlayWinSound()
    {
        if (sfxSource != null && playerJumpSFX != null)
        {
            float originalPitch = sfxSource.pitch;
            sfxSource.pitch = Random.Range(jumpPitchMin, jumpPitchMax);
            sfxSource.PlayOneShot(playerJumpSFX, sfxVolumeAdjusted());
            sfxSource.pitch = originalPitch;
        }
    }

    public void PlayPlayerJump()
    {
        if (sfxSource != null && playerJumpSFX != null)
        {
            float originalPitch = sfxSource.pitch;
            sfxSource.pitch = Random.Range(jumpPitchMin, jumpPitchMax);
            sfxSource.PlayOneShot(playerJumpSFX, sfxVolumeAdjusted());
            sfxSource.pitch = originalPitch;
        }
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
