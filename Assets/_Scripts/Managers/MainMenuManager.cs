using System.Net;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is needed by the Main Menu scene. It gives functionality to the buttons within the scene.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("Bool Parameters")]
    /*
     * These are our private listeners that we can visibly see within inspector, and also
     * assigning these to read-only properties for other scripts to check and interact with.
    */

    [SerializeField] private bool isOnMainMenu;
    public bool IsOnMainMenu => isOnMainMenu;

    [SerializeField] private bool isOnCredits;
    public bool IsOnCredits => isOnCredits;

    [SerializeField] private bool isOnOptions;
    private bool IsOnOptions => isOnOptions;

    [Header("Needed References")]
    [SerializeField] public GameObject mainMenuPanel;
    [SerializeField] public GameObject creditsPanel;
    [SerializeField] public GameObject optionsPanel;

    [SerializeField] private GameObject parallaxBG;

    [Header("Volume UI")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;



    /*
     *  Main Menu Button Functionality 
     *      -   Enables functionality of the new game, options, credits, and quit to desktop buttons.
    */
    public void StartANewGame()
    {
        if (isOnMainMenu)
        {
            AudioManager.Instance.PlayButtonClick();

            GameManager.Instance.StartNewGame();
        }
    }

    public void OpenMainMenu()
    {
        AudioManager.Instance.PlayButtonClick();

        isOnMainMenu = true;
        isOnOptions = false;
        isOnCredits = false;
        GameManager.Instance.ShowMenu();

        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        parallaxBG.SetActive(true);

        AudioManager.Instance.PlayMainMenuMusic();
    }

    public void OpenOptions()
    {
        AudioManager.Instance.PlayButtonClick();

        isOnMainMenu = false;
        isOnOptions = true;
        Debug.Log("Options button clicked!");
    }

    public void OpenCredits()
    {
        AudioManager.Instance.PlayButtonClick();

        isOnMainMenu = false;
        isOnCredits = true;
        Debug.Log("Opened Credits!");
        AudioManager.Instance.StopMusic();
    }

    public void QuitGameToDesktop()
    {
        AudioManager.Instance.PlayButtonClick();

        isOnMainMenu = false;
        // Quit the Application
        GameManager.Instance.QuitGame();
    }

    /*
     * This section are the button methods for within the Options panel.
     * So far only Fullscreen option is available.
    */
    public void ToggleFullScreen()
    {
        AudioManager.Instance.PlayButtonClick();
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("FullScreen Toggled!)");
    }

    public void OnMusicVolumeChanged(float newValue)
    {
        AudioManager.Instance.SetMusicVolume(newValue);
    }

    public void OnSFXVolumeChanged(float newValue)
    {
        AudioManager.Instance.SetSFXVolume(newValue);
    }


    private void Awake()
    {
        AudioManager.Instance.PlayMainMenuMusic();

        GameManager.Instance.ShowMenu();
        // Check to see if the gamemanager is currently displaying the main menu
        // If it is go ahead and display the scene
        if (GameManager.Instance.CurrentState == GameManager.GameState.Menu)
        {
            isOnMainMenu = true;
            mainMenuPanel.SetActive(true);
            optionsPanel.SetActive(false);
            creditsPanel.SetActive(false);
            parallaxBG.SetActive(true);
        }
        else
        {
            isOnMainMenu = false;
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = 1f;
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = 1f;
        }
    }

    private void Update()
    {
        if (isOnCredits && Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMainMenu();
        }

        if (isOnOptions)
        {
            mainMenuPanel.SetActive(false);

            optionsPanel.SetActive(true);
            creditsPanel.SetActive(false);

            parallaxBG.SetActive(false);
        }

        if (isOnCredits)
        {
            mainMenuPanel.SetActive(false);
            AudioManager.Instance.StopMusic();

            optionsPanel.SetActive(false);
            creditsPanel.SetActive(true);

            parallaxBG.SetActive(false);
        }
    }
}
