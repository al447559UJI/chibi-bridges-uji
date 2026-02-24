using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerActionData", menuName = "Scriptable Objects/PlayerActionData")]
public class PlayerActionData : ScriptableObject
{
    [Header("Melee attack parameters")]
    public int meleeDamage;
    public float meleeCooldown;

    [Header("Shoot attack parameters")]
    public int shootDamage;
    public float shootCooldown;

    [Header("Build mode parameters")]
    public float buildRange;
}
