using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public float spawnInterval = 4f;
    public  Vector2 areaSize = new Vector2(20f, 20f);
    private float spawnTimer = 0f;
    private int spawnedCount = 0;
    public int maxCollectibles = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnInterval)
        {
            spawnTimer = 0f;
            SpawnCollectible();
        }
    }
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
