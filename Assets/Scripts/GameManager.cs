using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;    // Single shared reference so other scripts can reach this GameManager easily
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI highScoreText;

    public float difficultyTimer = 0f;     // Counts up each frame, used to check when to increase difficulty
    public float difficultyInterval = 10f; // How often (in seconds) difficulty increases
    public float difficultyMultiplier = 1f; // Multiplies enemy speed - increases over time

    public GameObject pausePanel;
    private bool isPaused = false;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        // Difficulty ramps up automatically every 'difficultyInterval' seconds
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= difficultyInterval)
        {
            difficultyTimer = 0f;
            IncreaseDifficulty();
        }

        // Escape key toggles the pause menu on/off
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Adds to the player's score and updates the on-screen score UI
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score " + score);
        scoreText.text = "Score: " + score;
    }

    // Called when the player's health reaches zero - saves high score, updates UI, shows Game Over panel
    public void GameOver()
    {
        Debug.Log("Game Over triggered");
        AudioManager.instance.PlaySound(AudioManager.instance.hitSound);
        SaveManager.instance.SaveHighScore(score);
        highScoreText.text = "High Score: " + SaveManager.instance.LoadHighScore();
        gameOverPanel.SetActive(true);
    }

    // Resets score and reloads the Gameplay scene from scratch
    public void RestartGame()
    {
        Debug.Log("Restarting game");
        score = 0;
        SceneManager.LoadScene("Gameplay");
    }

    // Freezes/unfreezes gameplay using Time.timeScale, and shows/hides the pause panel
    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // Closes the application - only functions in a built .exe, not in the Unity Editor
    public void ExitGame()
    {
        Debug.Log("Exiting game");
        Application.Quit();
    }

    // Gradually increases enemy speed over time to ramp up challenge
    void IncreaseDifficulty()
    {
        difficultyMultiplier += 0.2f;
        Debug.Log("Difficulty increased: " + difficultyMultiplier);
    }
}