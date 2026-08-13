using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
