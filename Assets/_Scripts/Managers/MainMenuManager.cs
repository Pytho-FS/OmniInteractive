using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides functionality for the Main Menu buttons:
/// - Starting a new game,
/// - Opening the Options panel,
/// - Opening the Credits scene,
/// - Quitting the game,
/// - Toggling fullscreen,
/// - And adjusting volume.
/// This manager should only be active in the Main Menu scene.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private GameObject parallaxBG;

    [Header("Volume UI")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Awake()
    {
        // Only run this manager in the Main Menu scene.
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            gameObject.SetActive(false);
            return;
        }

        // Show Main Menu UI elements.
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        if (parallaxBG != null)
            parallaxBG.SetActive(true);

        // Initialize volume sliders if assigned.
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = 1f;
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = 1f;
    }

    public void StartANewGame()
    {
        AudioManager.Instance.PlayButtonClick();
        GameManager.Instance.StartNewGame();
    }

    public void OpenMainMenu()
    {
        AudioManager.Instance.PlayButtonClick();
        GameManager.Instance.ShowMenu();

        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        if (parallaxBG != null)
            parallaxBG.SetActive(true);

        GameManager.Instance.LoadScene("Main Menu");
    }

    public void OpenOptions()
    {
        AudioManager.Instance.PlayButtonClick();
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
        if (optionsPanel != null)
            optionsPanel.SetActive(true);
        if (parallaxBG != null)
            parallaxBG.SetActive(false);
    }

    public void OpenCredits()
    {
        AudioManager.Instance.PlayButtonClick();
        // Load the Credits scene.
        GameManager.Instance.LoadScene("Credits");
    }

    public void QuitGameToDesktop()
    {
        AudioManager.Instance.PlayButtonClick();
        GameManager.Instance.QuitGame();
    }

    /// <summary>
    /// Fully toggles fullscreen mode.
    /// </summary>
    public void ToggleFullScreen()
    {
        AudioManager.Instance.PlayButtonClick();
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Fullscreen toggled. Now fullscreen: " + Screen.fullScreen);
    }

    /// <summary>
    /// Called by the Music Volume slider's OnValueChanged event.
    /// </summary>
    public void OnMusicVolumeChanged(float newValue)
    {
        AudioManager.Instance.SetMusicVolume(newValue);
    }

    /// <summary>
    /// Called by the SFX Volume slider's OnValueChanged event.
    /// </summary>
    public void OnSFXVolumeChanged(float newValue)
    {
        AudioManager.Instance.SetSFXVolume(newValue);
    }

    private void Update()
    {
        // In Options mode, ensure Options panel remains visible.
        if (optionsPanel != null && optionsPanel.activeSelf)
        {
            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(false);
            if (parallaxBG != null)
                parallaxBG.SetActive(false);
        }
    }
}
