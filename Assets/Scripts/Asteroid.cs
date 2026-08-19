using UnityEngine;

public class Asteroid : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    // Damages the player and destroys the asteroid on impact
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}