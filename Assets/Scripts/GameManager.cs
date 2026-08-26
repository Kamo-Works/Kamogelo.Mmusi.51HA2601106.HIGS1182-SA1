using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI highScoreText;

    public int totalCollectiblesNeeded = 10;   // How many items must be collected to win
    public GameObject winPanel;                // Shown when the win condition is met

    public float difficultyTimer = 0f;
    public float difficultyInterval = 10f;
    public float difficultyMultiplier = 1f;

    public GameObject pausePanel;
    private bool isPaused = false;
    private bool gameEnded = false;            // True once the player has won or died, blocks pausing afterward

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= difficultyInterval)
        {
            difficultyTimer = 0f;
            IncreaseDifficulty();
        }

        // Escape toggles pause, but only if the game hasn't already ended (win/lose)
        if (Input.GetKeyDown(KeyCode.Escape) && !gameEnded)
        {
            TogglePause();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score " + score);
        scoreText.text = "Score: " + score;
        CheckWinCondition();
    }

    // Checks if enough items have been collected to win, and shows the Win panel if so
    public void CheckWinCondition()
    {
        if (score >= totalCollectiblesNeeded)
        {
            Debug.Log("All items collected - Player wins!");
            Time.timeScale = 0f;
            winPanel.SetActive(true);

            // Unlock the cursor so the player can click Restart/Exit on the Win panel
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over triggered");
        AudioManager.instance.PlaySound(AudioManager.instance.hitSound);
        SaveManager.instance.SaveHighScore(score);
        highScoreText.text = "High Score: " + SaveManager.instance.LoadHighScore();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        // Unlock the cursor so the player can click Restart/Exit on the Game Over panel
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game");
        score = 0;
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        // Unlock the cursor while paused so pause menu buttons are clickable,
        // relock it when resuming so mouse-look works again
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game");
        Application.Quit();
    }

    void IncreaseDifficulty()
    {
        difficultyMultiplier += 0.2f;
        Debug.Log("Difficulty increased: " + difficultyMultiplier);
    }
}