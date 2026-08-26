using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float driftSpeed = 1f;
    private Vector3 driftDirection;

    void Start()
    {
        // pick a random float direction to drift,in so each asteroid moves differently
        float angle = Random.Range(0f, 360f);
        driftDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    void Update()
    {
        transform.Translate(driftDirection * driftSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up, 10f * Time.deltaTime);
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