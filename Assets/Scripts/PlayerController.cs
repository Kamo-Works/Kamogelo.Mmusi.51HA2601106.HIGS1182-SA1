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
    public float mouseSensitivity = 3f;
    private float yaw;
    private float pitch;
    public float minPitch = -60f;
    public float maxPitch = 60f;
    private bool isDead = false;
    void Start()
    {
        yaw = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        if (isDead || Time.timeScale == 0f)
        {
            return;
        }
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

        MouseLook();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    // Spawns a laser projectile from the FirePoint and plays the shoot animation/sound
    // Fires a visible laser bolt for feedback, while using an instant raycast
    // to actually detect and destroy whatever was hit (asteroid or enemy)
    void Shoot()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Camera.main.transform.rotation);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Asteroid") || hit.collider.CompareTag("Enemy"))
            {
                // Calculate how long the visible laser bolt would actually take to
                // travel the distance to the target, then delay the destroy until then -
                // this keeps the raycast's instant accuracy but makes it LOOK like the
                // bullet is what caused the hit, not an invisible instant check
                float laserSpeed = laser.GetComponent<Projectile>().speed;
                float travelTime = hit.distance / laserSpeed;

                StartCoroutine(DestroyAfterDelay(hit.collider.gameObject, travelTime));
            }
        }

        Debug.Log("Player fired");
        animationController.TriggerShoot();
        AudioManager.instance.PlaySound(AudioManager.instance.shootSound);
    }

    // Waits for the calculated travel time, then destroys the target -
    // the null check guards against the target already being destroyed by something else
    // in the meantime (e.g. another laser hitting it first)
    IEnumerator DestroyAfterDelay(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (target != null)
        {
            Destroy(target);
            Debug.Log(target.name + " destroyed on laser impact");
        }
    }

    // Proper FPS-style mouse look: horizontal mouse movement turns the player (yaw),
    // vertical mouse movement tilts the view up/down (pitch), clamped so you can't flip
    // all the way around (like looking through your own body)
    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Only the player's body rotates left/right (yaw) - pitch is handled
        // separately by the camera, so the character model doesn't tilt oddly
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    // Lets other scripts (like CameraFollow) read the current up/down look angle
    public float GetPitch()
    {
        return pitch;
    }

    // Reduces health when the player is hit, and triggers death/game over once health runs out
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player health: " + health);
        AudioManager.instance.PlaySound(AudioManager.instance.hitSound);

        if (health <= 0)
        {
            isDead = true;
            animationController.TriggerDie();
            GameManager.instance.GameOver();
        }
    }
}