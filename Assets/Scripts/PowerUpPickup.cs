using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    // Activates the thruster boost on the player and removes this power-up when collected
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PowerUpManager>().ActivateThrusterBoost();
            Debug.Log("Thruster power-up collected");
            Destroy(gameObject);
        }
    }
}