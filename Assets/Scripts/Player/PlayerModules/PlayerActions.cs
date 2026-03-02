using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private PlayerActionData data;
    [SerializeField] private PlayerMeleeVisual meleeVisual;
    [SerializeField] private PlayerMeleeHitbox meleeHitbox;
    [SerializeField] private Transform firePoint;
    
    private PlayerInput input;
    private PlayerMovement movement;
    private float lastMeleeAnimationStartTime;
    private float lastShootStartTime;
    private bool isMeleeActive;

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

    public void Shoot()
    {
        if (Time.time - lastShootStartTime >= data.shootCooldown)
        {
            lastShootStartTime = Time.time;
            GameObject bulletGameObject = PlayerBulletPool.instance.GetPooledObject();

            if (bulletGameObject != null)
            {
                bulletGameObject.transform.position = firePoint.position;
                bulletGameObject.SetActive(true);
    
                bulletGameObject.GetComponent<PlayerBullet>().Initialize(movement.facingDirection, data.projectileSpeed);
            }
        }
    }
}
