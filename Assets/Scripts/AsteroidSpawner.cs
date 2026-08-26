using UnityEngine;
using UnityEngine.Rendering;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 3f;
    public float spawnRadius = 15f;
    private float spawnTimer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnAsteroid();
        }
    }
    void SpawnAsteroid()
    {
        // Pick a random point on a circle around the spawner
        float angle = Random.Range(0f, 360f);
        float radians = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(radians) * spawnRadius;
        float z = Mathf.Sin(radians) * spawnRadius;

        Vector3 spawnPosition = transform.position + new Vector3(x, 1f, z);

        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Asteroid spawned at " + spawnPosition);
    }
}
