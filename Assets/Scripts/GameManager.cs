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
    public float difficultyTimer = 0f;
    public float difficultyInterval = 10f;
    public float difficultyMultiplier = 1f;
    public GameObject pausePanel;
    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        difficultyTimer += Time.deltaTime;

        if (difficultyTimer >= difficultyInterval)
        {
            difficultyTimer = 0f;
            IncreaseDifficulty();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score " + score);
        scoreText.text = "Score: " + score;
    }
   
    public void GameOver()
    {
        Debug.Log("Game Over triggered");
        AudioManager.instance.PlaySound(AudioManager.instance.hitSound);
        SaveManager.instance.SaveHighScore(score);
        highScoreText.text = "High Score: " + SaveManager.instance.LoadHighScore();
        gameOverPanel.SetActive(true);
    }
    public void RestartGame()
    {
        Debug.Log("Restarting game");
        score = 0;
        SceneManager.LoadScene("Gameplay");
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
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
