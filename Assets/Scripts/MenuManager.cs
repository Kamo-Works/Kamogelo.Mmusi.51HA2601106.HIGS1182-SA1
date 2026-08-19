using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        // Show the saved high score as soon as the Main Menu loads
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update()
    {

    }

    // Called by the Start button - loads the Gameplay scene
    public void StartGame()
    {
        Debug.Log("Loading Gameplay scene");
        SceneManager.LoadScene("Gameplay");
    }
}