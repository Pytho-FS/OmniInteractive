using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Menu, Playing, Paused, GameOver, Win }
    public GameState CurrentState { get; private set; } = GameState.Playing;

    private int currentMiniGame = 0;
    private int totalMiniGames = 3;

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
        if (currentMiniGame < totalMiniGames)
        {
            currentMiniGame++;
            Debug.Log("Starting MiniGame " + currentMiniGame + " of " + totalMiniGames);
        }
        else
        {
            WinGame();
        }
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
    }

    public int GetCurrentMiniGame()
    {
        return currentMiniGame;
    }
}


