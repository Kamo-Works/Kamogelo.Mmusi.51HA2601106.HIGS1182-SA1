using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Cache the Animator component so we don't call GetComponent repeatedly
        animator = GetComponent<Animator>();
    }

    void Update()
    {
    }

    // Switches between Idle/Walk animation states based on whether the player is currently moving
    public void SetMoving(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    // Plays the shoot animation once
    public void TriggerShoot()
    {
        animator.SetTrigger("Shoot");
    }

    // Plays the death animation once
    public void TriggerDie()
    {
        animator.SetTrigger("Die");
    }
}