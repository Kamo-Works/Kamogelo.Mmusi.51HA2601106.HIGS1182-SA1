using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                              // The player the camera should follow
    public Vector3 offset = new Vector3(0f, 4f, -6);       // Distance/height behind the player

    void Start()
    {

    }

    // Keeps the camera positioned behind the player, rotating along with the player's facing
    // direction, and always looking at the player
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 rotatedOffset = target.rotation * offset;
            transform.position = target.position + rotatedOffset;
            transform.LookAt(target.position);
        }
    }
}