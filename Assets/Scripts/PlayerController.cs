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

    void Start()
    {
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

        bool isMoving = horizontal != 0f || vertical != 0f;
        animationController.SetMoving(isMoving);

        RotateTowardsMouse();

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

    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            Vector3 direction = new Vector3(pointToLook.x, transform.position.y, pointToLook.z) - transform.position;

            if (direction.magnitude > 0.5f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
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