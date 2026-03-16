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
    public float directionTimer = 1;

    [Header("Health parameters")]
    public int health = 5;
}