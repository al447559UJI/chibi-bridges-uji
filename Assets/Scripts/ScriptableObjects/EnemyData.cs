using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Movement parameters")]
    [Range(0.25f, 20f)]
    public float maxSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    [Tooltip("How many seconds until flipping direction.")]
    public float directionTimer = 1f;

    [Header("Health / damage parameters")]
    public int health = 5;
    public int shootDamage = 1;
    public float projectileSpeed;
    
    [Tooltip("Time between the enemy reacting to the player and shooting.")]
    public float reactionCooldown = .75f;
    public float shootCooldown = 1f;
    public int meleeDamage = 1;

    [Header("Search parameters")]
    public float searchDistance = 5f;
}