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

    public bool canBuild { get; private set; }

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
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
    }

    public void ShowPoleIndicator()
    {
        poleUI.Show();
    }

    public void HidePoleIndicator()
    {
        poleUI.Hide();
    }

    public void SetCanBuild(bool value)
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
