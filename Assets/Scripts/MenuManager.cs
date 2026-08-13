using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI highScoreText;
    public void GameOver()
    {
        Debug.Log("Game Over triggered");
        gameOverPanel.SetActive(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        Debug.Log("Loading Gameplay scene");
        SceneManager.LoadScene("Gameplay");
    }
}
