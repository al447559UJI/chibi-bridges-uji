using UnityEngine;

public class EnemyStatusBubble : MonoBehaviour
{

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayWarningAnimation()
    {
        animator.Play("StatusWarning");
    }

        public void PlayEmptyAnimation()
    {
        animator.Play("StatusEmpty");
    }
}
