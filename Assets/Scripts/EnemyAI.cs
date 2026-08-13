using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform player;
    private bool canDamage = true;
    public float damageCooldown = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = moveSpeed * GameManager.instance.difficultyMultiplier;
        transform.position = Vector3.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            other.GetComponent<PlayerController>().TakeDamage(1);
            Debug.Log("Enemy hit player");
            canDamage = false;
            Invoke(nameof(ResetDamage), damageCooldown);
        }
    }

    void ResetDamage()
    {
        canDamage = true;
    }
}
