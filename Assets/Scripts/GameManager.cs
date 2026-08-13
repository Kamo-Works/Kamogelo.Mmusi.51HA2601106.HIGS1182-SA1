using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
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
    public TextMeshProUGUI scoreText;
}
