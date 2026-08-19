using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform player;
    private bool canDamage = true;
    public float damageCooldown = 1f;
    public float viewRadius = 10f;
    public float viewAngle = 90f;
      

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {
            float currentSpeed = moveSpeed * GameManager.instance.difficultyMultiplier;
            transform.position = Vector3.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);
        }
       
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
    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (player.position -transform.position).normalized;
        
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
