using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Menu, Playing, Paused, GameOver, Win }
    public GameState CurrentState { get; private set; } = GameState.Playing;

    private int currentMiniGame = 0;
    private int totalMiniGames = 3;
    public int totalNinjas = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowMenu();
        //ResumeGame();
    }
    public void StartNextMiniGame()
    {
        if (currentMiniGame < totalMiniGames - 1) 
        {
            currentMiniGame++;
            Debug.Log("Starting MiniGame " + currentMiniGame + " of " + totalMiniGames);

            // Notify CameraController to update position
            CameraController.Instance.MoveToMiniGame();
        }
        else
        {
            WinGame();
        }
    }


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAndSetMusic(sceneName));
    }

    private IEnumerator LoadSceneAndSetMusic(string sceneName)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOp.isDone)
        {
            yield return null;
        }

        if (sceneName != "MainMenu")
        {
            yield return new WaitForSeconds(3f);
            AudioManager.Instance.PlayBackgroundMusicForScene(sceneName);
        }
    }

    public void StartNewGame()
    {
        CurrentState = GameState.Playing;
        SceneManager.LoadScene("Main Scene"); // load into the game
    }
    public void PauseGame()
    {
        CurrentState = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        Debug.Log("Game Over!");
    }

    public void WinGame()
    {
        CurrentState = GameState.Win;

        if (CurrentState == GameState.Win)
        {
            GameManager.Instance.LoadScene("Credits");
        }
        Debug.Log("Game Win!");
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowMenu()
    {
        CurrentState = GameState.Menu;
        Debug.Log("Showing Menu (Placeholder). Implement menu logic here.");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public int GetCurrentMiniGame()
    {
        return currentMiniGame;
    }

    public void SetCurrentMiniGame(int miniGameIndex)
    {
        currentMiniGame = miniGameIndex;
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Credits")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.QuitGame();
            }
        }
    }
}



