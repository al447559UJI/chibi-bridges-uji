using UnityEngine;

[CreateAssetMenu(fileName = "PlayerActionData", menuName = "Scriptable Objects/PlayerActionData")]
public class PlayerActionData : ScriptableObject
{
    [Header("Debug toggles")]
    public bool debugInfiniteAmmo = false;
    
    [Header("Melee attack parameters")]
    public int meleeDamage;
    [Range(0.22f, 1.5f)]
    public float meleeCooldown;

    [Header("Shoot attack parameters")]
    public int shootDamage;
    public float shootCooldown;
    public int shootCost = 10;
    public float projectileSpeed;

    [Header("Build mode parameters")]
    public float buildRange;
    public int poleDamage = 1000;
    public int poleCost = 100;

    [Header("Scrap parameters")]
    public int maxScrapAmount = 1000;
    public int initialScrapAmount = 100;
    public int scrapCollectAmount = 100;
}
