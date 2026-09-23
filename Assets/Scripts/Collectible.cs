using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 1;   // How much score this collectible is worth

    void Start()
    {

    }

    void Update()
    {

    }

    // Adds score, plays pickup sound, and removes the object when the player touches it
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore(scoreValue);
            Debug.Log("Collectible picked up");
            AudioManager.instance.PlaySound(AudioManager.instance.pickupSound);
            GameManager.instance.ShowFeedback("+" + scoreValue + " Scrap Collected!");
            Destroy(gameObject);
        }
    }
}