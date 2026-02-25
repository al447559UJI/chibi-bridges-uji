using UnityEngine;

[CreateAssetMenu(fileName = "PlayerActionData", menuName = "Scriptable Objects/PlayerActionData")]
public class PlayerActionData : ScriptableObject
{
    [Header("Melee attack parameters")]
    public int meleeDamage;
    public float meleeCooldown;
    public float meleeAttackDuration;
    public float meleeHitboxStartTime;
    public float meleeHitboxEndTime;

    [Header("Shoot attack parameters")]
    public int shootDamage;
    public float shootCooldown;

    [Header("Build mode parameters")]
    public float buildRange;
}
