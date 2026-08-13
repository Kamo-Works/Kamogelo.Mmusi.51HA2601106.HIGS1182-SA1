using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public float boostMultiplier = 2f;
    public float boostDuration = 5f;
    private PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateThrusterBoost()
    {
        StartCoroutine(ThrusterBoostRoutine());
    }

    private IEnumerator ThrusterBoostRoutine()
    {
        Debug.Log("Thruster boost activated");
        playerController.moveSpeed *= boostMultiplier;

        yield return new WaitForSeconds(boostDuration);

        playerController.moveSpeed /= boostMultiplier;
        Debug.Log("Thruster boost ended");
    }
}
