using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Child References")]
    [SerializeField] private PlayerMeleeAttack meleeAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PoleUI poleUI;

    [Header("Data & Prefabs")]
    [SerializeField] private PlayerActionData data;
    [SerializeField] private GameObject pole;
    [Header("Layer Settings")]
    [SerializeField] private LayerMask damageableLayer;
    [Header("Sound Assets")]
    [SerializeField] private AudioClip shootSound;

    private PlayerMovement movement;
    private PlayerScrap playerScrap;
    private Animator animator;
    private float lastMeleeAnimationStartTime;
    private float lastShootStartTime;
    private bool isMeleeAnimationPlaying;
    private bool isPolePositionValid;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        playerScrap = GetComponent<PlayerScrap>();
        animator = GetComponent<Animator>();
    }

    public void AirMeleeAttack()
    {
        meleeAttack.Render(movement.isGrounded);
        animator.SetBool("isAttacking", true);
        meleeAttack.InitializeHitbox(data.meleeDamage, damageableLayer);
    }

    public void MeleeAttack()
    {
        if (Time.time - lastMeleeAnimationStartTime >= data.meleeCooldown)
        {
            lastMeleeAnimationStartTime = Time.time;
            isMeleeAnimationPlaying = true;
            animator.SetBool("isAttacking", true);
            meleeAttack.Render(movement.isGrounded);
            meleeAttack.InitializeHitbox(data.meleeDamage, damageableLayer);
        }
    }

    public void OnMeleeAnimationEnded()
    {
        isMeleeAnimationPlaying = false;
        animator.SetBool("isAttacking", false);
    }

    public bool IsMeleeAnimationPlaying()
    {
        return isMeleeAnimationPlaying;
    }

    public void HideMeleeVisual()
    {
        meleeAttack.Hide();
    }

    public void Shoot()
    {

        if (Time.time - lastShootStartTime >= data.shootCooldown)
        {
            if (playerScrap.currentAmount >= data.shootCost || data.debugInfiniteAmmo)
            {
                lastShootStartTime = Time.time;
                GameObject newBullet = PlayerBulletPool.instance.GetPooledObject();

                if (newBullet != null)
                {
                    playerScrap.Spend(data.shootCost);
                    newBullet.transform.position = firePoint.position;
                    newBullet.SetActive(true);
                    newBullet.GetComponent<PlayerBullet>().Initialize(
                        movement.facingDirection,
                        data.projectileSpeed,
                        data.shootDamage);
                    SoundManager.instance.PlaySound(shootSound, transform.position, .5f);
                }
            }
            else
            {
                Debug.Log("Not enough scrap! " + playerScrap.currentAmount + " remaining.");
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

    public void SetPolePositionValid(bool isPositionValid)
    {

        if (isPositionValid && playerScrap.CanAffordPole())
        {
            poleUI.SetGreenColor();
        }
        else
        {
            poleUI.SetRedColor();
        }

        isPolePositionValid = isPositionValid;
    }

    /// <summary>
    /// Builds a pole at the current indicator position if placement is valid and has enough scrap.
    /// </summary>
    public void Build()
    {
        if (playerScrap.CanAffordPole() && isPolePositionValid)
        {
            GameObject newPole = Instantiate(pole, poleUI.GetSpawnPoint(), Quaternion.identity);
            if (newPole != null)
            {
                playerScrap.Spend(data.poleCost);
                newPole.GetComponent<Pole>()?.Initialize(movement.facingDirection, data.poleDamage);
            }
        }
    }

}
