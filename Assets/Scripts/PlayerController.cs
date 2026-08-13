using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject laserPrefab;
    public Transform firePoint;
    public int health = 3;
    // ← lives here, outside any method

    void Start()
    {

    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Player fired");
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player health: " + health);

        if (health <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}