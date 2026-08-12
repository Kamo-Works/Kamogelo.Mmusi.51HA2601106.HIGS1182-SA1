using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject ,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnAnimatorIK(int layerIndex)
    {
        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Laser hit: " + other.name);
            Destroy(gameObject);
        } 
    }
}
