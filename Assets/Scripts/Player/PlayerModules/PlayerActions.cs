using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private PlayerActionData data;
    [SerializeField] private PlayerMeleeAttack meleeAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PoleUI poleUI;
    [SerializeField] private GameObject pole;
    [SerializeField] private LayerMask damageableLayer;

    private PlayerInput input;
    private PlayerMovement movement;
    private float lastMeleeAnimationStartTime;
    private float lastShootStartTime;
    private bool isMeleeActive;

    public bool canBuild { get; private set; }

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
    }

    public void MeleeAttack()
    {
        if (Time.time - lastMeleeAnimationStartTime >= data.meleeCooldown)
        {
            lastMeleeAnimationStartTime = Time.time;
            isMeleeActive = true;
            meleeAttack.Render();
            meleeAttack.InitializeHitbox(data.meleeDamage, damageableLayer);
        }
        if (movement.isGrounded)
        {
            input.LockHorizontalMovement();
            input.LockJump();
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
        meleeAttack.Hide();
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
