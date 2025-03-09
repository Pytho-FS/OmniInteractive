using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; } = GameState.Playing;

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
        ResumeGame();
    }

    public void PauseGame()
    {
        CurrentState = GameState.Paused;
        Time.timeScale = 0f; // Pause physics and animations
    }

    public void ResumeGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f; // Resume game
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        Debug.Log("Game Over!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}


