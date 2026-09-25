using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform player;
    private bool hasSpottedPlayer = false;

    // --- Adapted from Sebastian Lague's Field of View system (github.com/SebLague/Field-of-View) ---
    // Original script detected multiple targets via a layer mask and returned a list of visible targets.
    // Simplified here into a single true/false check (CanSeePlayer) against one known target,
    // since this enemy only ever needs to know if it can see the Player specifically.
    // The original's obstacle raycast/layer mask was removed since this scene has no obstacle
    // layers set up yet - the adapted version only checks angle and distance.
    public float viewRadius = 10f;
    public float viewAngle = 90f;

    void Start()
    {
        // Find the Player once at the start rather than searching every frame
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        bool canSee = CanSeePlayer();

        // Log only on the moment detection changes, not every single frame,
        // so the Console isn't spammed while the enemy is chasing
        if (canSee && !hasSpottedPlayer)
        {
            Debug.Log("Enemy Drone Spotted Player");
            hasSpottedPlayer = true;
        }
        else if (!canSee && hasSpottedPlayer)
        {
            hasSpottedPlayer = false;
        }

        if (canSee)
        {
            float currentSpeed = moveSpeed * GameManager.instance.difficultyMultiplier;
            transform.position = Vector3.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);
        }
    }

    // Colliding with the enemy kills the player instantly (unlike asteroids,
    // which only deal partial damage) - reflects the enemy being a lethal threat
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy caught player - instant death");
            GameManager.instance.ShowFeedback("Caught by Enemy Drone!");
            other.GetComponent<PlayerController>().TakeDamage(999);
        }
    }


    // Returns true if the player is within the enemy's view angle and view radius
    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
        {
            float dstToPlayer = Vector3.Distance(transform.position, player.position);
            if (dstToPlayer < viewRadius)
            {
                return true;
            }
        }

        return false;
    }
}