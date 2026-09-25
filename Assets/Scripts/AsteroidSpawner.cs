using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;      // The Asteroid prefab to spawn
    public float spawnInterval = 3f;       // Time in seconds between each spawn
    public float spawnRadius = 15f;        // How far from the spawner asteroids can appear
    private float spawnTimer = 0f;

    void Start()
    {

    }

    void Update()
    {
        // Count up each frame, and spawn a new asteroid once the interval is reached
        spawnTimer += Time.deltaTime;
        float currentInterval = spawnInterval / GameManager.instance.difficultyMultiplier;

        if (spawnTimer >= currentInterval)
        {
            spawnTimer = 0f;
            SpawnAsteroid();
        }
    }

    // Spawns an asteroid at a random point on a circle around this spawner's position
    void SpawnAsteroid()
    {
        float angle = Random.Range(0f, 360f);
        float radians = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(radians) * spawnRadius;
        float z = Mathf.Sin(radians) * spawnRadius;

        Vector3 spawnPosition = transform.position + new Vector3(x, 1f, z);

        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Asteroid spawned at " + spawnPosition);
    }
}