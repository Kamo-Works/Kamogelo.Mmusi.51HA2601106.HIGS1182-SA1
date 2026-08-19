using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;           // How fast the player moves per second
    public GameObject laserPrefab;         // The laser prefab spawned when shooting
    public Transform firePoint;            // Marks where lasers spawn from (front of the player)
    public int health = 3;                 // Player's current health, starts at 3
    public AnimationController animationController; // Reference to the character's animation script

    void Start()
    {
    }

    void Update()
    {
        // Read movement input each frame
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move relative to the player's current facing direction (Space.Self),
        // so movement follows wherever the player has rotated to via the mouse
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

        // Tell the Animator whether to play the walk or idle animation
        bool isMoving = horizontal != 0f || vertical != 0f;
        animationController.SetMoving(isMoving);

        RotateTowardsMouse();

        // Fire a laser when Space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    // Spawns a laser projectile from the FirePoint and plays the shoot animation/sound
    void Shoot()
    {
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Player fired");
        animationController.TriggerShoot();
        AudioManager.instance.PlaySound(AudioManager.instance.shootSound);
    }

    // Rotates the player smoothly to face wherever the mouse cursor is pointing on the ground plane
    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            Vector3 direction = new Vector3(pointToLook.x, transform.position.y, pointToLook.z) - transform.position;

            // Ignore tiny/unstable directions when the cursor is very close to the player,
            // which otherwise causes erratic spinning
            if (direction.magnitude > 0.5f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    // Reduces health when the player is hit, and triggers death/game over once health runs out
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