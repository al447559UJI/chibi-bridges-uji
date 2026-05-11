using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAltData", menuName = "Scriptable Objects/EnemyAltData")]
public class EnemyAltData : ScriptableObject
{
    [Header("Movement parameters")]
    [Range(0.25f, 20f)]
    public float maxSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;

    [Header("Health / damage parameters")]
    public int health = 5;
    
    [Tooltip("Time between the enemy reacting to the player and shooting.")]
    public float reactionCooldown = .75f;
    public int meleeDamage = 1;
}

