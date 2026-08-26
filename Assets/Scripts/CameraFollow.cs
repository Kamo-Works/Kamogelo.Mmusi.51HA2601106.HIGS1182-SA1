using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                              // The player the camera should follow
    public Vector3 offset = new Vector3(0f, 4f, -6);       // Distance/height behind the player
    private PlayerController playerController;
  void Start()
    {
        playerController = target.GetComponent<PlayerController>();
    }

    // Keeps the camera positioned behind the player, rotating along with the player's facing
    // direction, and always looking at the player
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + target.rotation * offset;

            // Combine the player's yaw (left/right facing) with their separate pitch
            // (up/down look) to get the camera's final rotation
            transform.rotation = target.rotation * Quaternion.Euler(playerController.GetPitch(), 0f, 0f);
        }
    }

}
