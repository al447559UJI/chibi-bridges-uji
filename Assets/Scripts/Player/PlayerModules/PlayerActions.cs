using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent<int> onScrapChanged;
    public UnityEvent<string> onScrapGiven;
    public UnityEvent<string> onScrapSpent;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();

        // This has to be set before the HUD loads so the UI displays the correct number.
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
            if (currentScrapAmount >= data.shootCost || data.debugInfiniteAmmo)
            {
                lastShootStartTime = Time.time;
                GameObject newBullet = PlayerBulletPool.instance.GetPooledObject();

                if (newBullet != null)
                {
                    SpendScrap(data.shootCost);
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
        if (CanAffordPole() && isPolePositionValid)
        {
            GameObject newPole = Instantiate(pole, poleUI.GetSpawnPoint(), Quaternion.identity);
            if (newPole != null)
            {
                SpendScrap(data.poleCost);
                newPole.GetComponent<Pole>()?.Initialize(movement.facingDirection, data.poleDamage);
            }
        }
    }

    public void GiveScrap()
    {
        currentScrapAmount += data.scrapCollectAmount;
        onScrapChanged.Invoke(currentScrapAmount);
        onScrapGiven.Invoke($"+{data.scrapCollectAmount} Scrap");
    }

    private void SpendScrap(int amount)
    {
        if (data.debugInfiniteAmmo) return;

        currentScrapAmount -= amount;
        onScrapChanged.Invoke(currentScrapAmount);
        onScrapSpent.Invoke($"-{amount} Scrap");

        if (!CanAffordPole())
        {
            poleUI.SetRedColor();
        }
    }

    private bool CanAffordPole()
    {
        return currentScrapAmount >= data.poleCost;
    }
}
