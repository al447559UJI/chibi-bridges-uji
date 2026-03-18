using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private PlayerActionData data;
    [SerializeField] private PlayerMeleeVisual meleeVisual;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PoleUI poleUI;
    [SerializeField] private GameObject pole;
    [SerializeField] private BoxCollider2D meleeHitbox;
    
    private PlayerInput input;
    private PlayerMovement movement;
    private float lastMeleeAnimationStartTime;
    private float lastShootStartTime;
    private bool isMeleeActive;
    
    public bool canBuild {get; private set;}

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
    }

    public void MeleeAttack()
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

    private void InitializeMeleeHitbox()
    {
        Physics2D.BoxCastAll(
            meleeHitbox.transform.position,
            meleeHitbox.size,
            0f,
            Vector2.right
            );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(meleeHitbox.transform.position, meleeHitbox.size);
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
            GameObject newBullet = PlayerBulletPool.instance.GetPooledObject();

            if (newBullet != null)
            {
                newBullet.transform.position = firePoint.position;
                newBullet.SetActive(true);
    
                newBullet.GetComponent<PlayerBullet>().Initialize(movement.facingDirection, data.projectileSpeed, data.shootDamage);
            }
        }
    }

    public void ShowPoleIndicator()
    {
        poleUI.Show();
    }

    public void HidePoleIndicator()
    {
        poleUI.Hide();
    }

    public void CanBuild(bool value)
    {
        canBuild = value;
    }

    public void Build()
    {
        if (canBuild)
        {
            GameObject newPole = Instantiate(pole, poleUI.GetSpawnPoint(), Quaternion.identity);
            if (newPole != null)
            {
                newPole.GetComponent<Pole>().Initialize(movement.facingDirection, data.poleDamage);
            }
        }
    }
}
