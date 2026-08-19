using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public float boostMultiplier = 2f;   // How much to multiply the player's speed by during the boost
    public float boostDuration = 5f;     // How long the boost lasts, in seconds
    private PlayerController playerController;

    void Start()
    {
        // Cache a reference to PlayerController once, rather than calling GetComponent repeatedly
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {

    }

    // Called externally (by PowerUpPickup) when the player collects a thruster power-up
    public void ActivateThrusterBoost()
    {
        StartCoroutine(ThrusterBoostRoutine());
    }

    // Temporarily increases the player's move speed, then reverts it after boostDuration seconds.
    // Uses a coroutine so the game can "wait" without freezing anything else.
    private IEnumerator ThrusterBoostRoutine()
    {
        Debug.Log("Thruster boost activated");
        playerController.moveSpeed *= boostMultiplier;

        yield return new WaitForSeconds(boostDuration);

        playerController.moveSpeed /= boostMultiplier;
        Debug.Log("Thruster boost ended");
    }
}