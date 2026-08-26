using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;

    void Start()
    {
        // Launch the laser forward using physics velocity, and auto-destroy after 3 seconds
        // so it doesn't fly forever and clutter the scene
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, 3f);
    }

    void Update()
    {

    }

    
}