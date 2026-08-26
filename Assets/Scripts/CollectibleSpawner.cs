using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;       // The Collectible prefab to spawn
    public float spawnInterval = 4f;           // Time in seconds between each spawn attempt
    public Vector2 areaSize = new Vector2(20f, 20f); // Width/depth of the spawn area
    private float spawnTimer = 0f;
    private int spawnedCount = 0;
    public int maxCollectibles = 10;           // Total collectibles allowed before spawning stops

    void Start()
    {

    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnInterval)
        {
            spawnTimer = 0f;
            SpawnCollectible();
        }
    }

    // Spawns a collectible at a random position within a rectangular area,
    // stopping once maxCollectibles has been reached (supports the win condition)
    void SpawnCollectible()
    {
        if (spawnedCount >= maxCollectibles)
        {
            return;
        }

        float randomX = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float randomZ = Random.Range(-areaSize.y / 2, areaSize.y / 2);

        Vector3 spawnPosition = transform.position + new Vector3(randomX, 0.5f, randomZ);

        Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
        spawnedCount++;
        Debug.Log("Collectible spawned at " + spawnPosition + " (" + spawnedCount + "/" + maxCollectibles + ")");
    }
}