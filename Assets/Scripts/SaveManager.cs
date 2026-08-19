using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;    // Single shared reference so other scripts can save/load easily

    void Start()
    {
        instance = this;
    }

    void Update()
    {

    }

    // Saves the given score as the new high score, but only if it beats the current one
    public void SaveHighScore(int score)
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            Debug.Log("New high score saved: " + score);
        }
    }

    // Returns the currently saved high score, or 0 if none has been saved yet
    public int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}