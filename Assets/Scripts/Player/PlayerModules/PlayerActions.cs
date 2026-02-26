using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private PlayerActionData data;
    [SerializeField] private PlayerMeleeVisual meleeVisual;
    [SerializeField] private PlayerMeleeHitbox meleeHitbox;


    public bool isMeleeActive {get; private set; }
    
    private PlayerInput input;
    private PlayerMovement movement;
    private float lastMeleeAnimationStartTime;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
    }

    public void PlayMeleeAnimation()
    {

        if (movement.isGrounded)
        {
            input.LockHorizontalMovement();
            input.LockJump();
        }
        if (Time.time - lastMeleeAnimationStartTime >= data.meleeCooldown)
        {
            lastMeleeAnimationStartTime = Time.time;
            isMeleeActive = true;
            meleeVisual.Render();
        }
    }

    public void OnMeleeAnimationEnded()
    {
        input.UnlockHorizontalMovement();
        input.UnlockJump();
        isMeleeActive = false;
    }

    public bool IsMeleeActive()
    {
        return isMeleeActive;
    }

    public void HideMeleeVisual()
    {
        meleeVisual.Hide();
    }
}
