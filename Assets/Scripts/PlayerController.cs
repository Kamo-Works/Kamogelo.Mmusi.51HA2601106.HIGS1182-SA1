using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject laserPrefab;
    public Transform firePoint;
    public int health = 3;
    public AnimationController animationController;
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
        bool isMoving = horizontal !=0f || vertical !=0f;
        animationController.SetMoving(isMoving);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Player fired");
        animationController.TriggerShoot();
        AudioManager.instance.PlaySound(AudioManager.instance.shootSound);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player health: " + health);
        AudioManager.instance.PlaySound(AudioManager.instance.hitSound);

        if (health <= 0)
        {
            animationController.TriggerDie();
            GameManager.instance.GameOver();
           
        }
    }

}