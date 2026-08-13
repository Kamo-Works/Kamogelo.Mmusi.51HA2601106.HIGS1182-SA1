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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
