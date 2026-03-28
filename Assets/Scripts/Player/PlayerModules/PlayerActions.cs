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

    private PlayerMovement movement;
    private float lastMeleeAnimationStartTime;
    private float lastShootStartTime;
    private bool isMeleeAnimationPlaying;
    private bool isPolePositionValid;

    public int currentScrapAmount { get; private set; }
    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        DebugRegistry.Register("Current scrap", () => currentScrapAmount);
        currentScrapAmount = data.initialScrapAmount;
    }

    public void AirMeleeAttack()
    {
        meleeAttack.Render();
        meleeAttack.InitializeHitbox(data.meleeDamage, damageableLayer);
    }

    public void MeleeAttack()
    {
        if (Time.time - lastMeleeAnimationStartTime >= data.meleeCooldown)
        {
            lastMeleeAnimationStartTime = Time.time;
            isMeleeAnimationPlaying = true;
            meleeAttack.Render();
            meleeAttack.InitializeHitbox(data.meleeDamage, damageableLayer);
        }
    }

    public void OnMeleeAnimationEnded()
    {
        isMeleeAnimationPlaying = false;
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
            if (currentScrapAmount >= data.shootCost)
            {
                lastShootStartTime = Time.time;
                GameObject newBullet = PlayerBulletPool.instance.GetPooledObject();

                if (newBullet != null)
                {
                    newBullet.transform.position = firePoint.position;
                    newBullet.SetActive(true);
                    newBullet.GetComponent<PlayerBullet>().Initialize(
                        movement.facingDirection,
                        data.projectileSpeed,
                        data.shootDamage);
                }
            }
            else
            {
                Debug.Log("Not enough scrap! " + currentScrapAmount + " remaining.");
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

        if (isPositionValid && CanAffordPole())
        {
            poleUI.SetColor(Color.green);
        }
        else
        {
            poleUI.SetColor(Color.red);

        }

        isPolePositionValid = isPositionValid;

    }

    /// <summary>
    /// Builds a pole at the current indicator position if placement is valid and has enough scrap.
    /// </summary>
    public void Build()
    {
        if (CanAffordPole() && isPolePositionValid)
        {
            GameObject newPole = Instantiate(pole, poleUI.GetSpawnPoint(), Quaternion.identity);
            if (newPole != null)
            {
                SpendScrap(data.poleCost);
                newPole.GetComponent<Pole>().Initialize(movement.facingDirection, data.poleDamage);
            }
        }
    }

    public void GiveScrap()
    {
        currentScrapAmount += data.scrapCollectAmount;
    }

    private void SpendScrap(int amount)
    {
        currentScrapAmount -= amount;

        if (!CanAffordPole())
        {
            poleUI.SetColor(Color.red);
        }
    }

    private bool CanAffordPole()
    {
        return currentScrapAmount >= data.poleCost;
    }

}
