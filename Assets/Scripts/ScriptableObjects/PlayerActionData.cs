using UnityEngine;

[CreateAssetMenu(fileName = "PlayerActionData", menuName = "Scriptable Objects/PlayerActionData")]
public class PlayerActionData : ScriptableObject
{
    [Header("Melee attack parameters")]
    public int meleeDamage;
    [Range(0.22f, 1.5f)]
    public float meleeCooldown;

    [Header("Shoot attack parameters")]
    public int shootDamage;
    public float shootCooldown;
    public float projectileSpeed;

    [Header("Build mode parameters")]
    public float buildRange;
    public int poleDamage = 1000;
}
