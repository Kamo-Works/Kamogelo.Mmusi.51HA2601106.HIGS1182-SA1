using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore(scoreValue);
            Debug.Log("Collectible picked up");
            AudioManager.instance.PlaySound(AudioManager.instance.pickupSound);
            Destroy(gameObject);
           
        }
    }
}
