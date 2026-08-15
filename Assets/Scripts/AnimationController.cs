using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void SetMoving(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    public void TriggerShoot()
    {
        animator.SetTrigger("Shoot");
    }

    public void TriggerDie()
    {
        animator.SetTrigger("Die");
    }
}