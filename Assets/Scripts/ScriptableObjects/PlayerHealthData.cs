using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealthData", menuName = "Scriptable Objects/PlayerHealthData")]
public class PlayerHealthData : ScriptableObject
{
    [Header("Debug toggles")]
    public bool debugGodMode = false;

    [Header("Health parameters")]
    public int maxHealth = 100;

    [Header("Invencibility parameters")]
    public float invencibleTime = .5f;
    [Tooltip("Percentage of invincibility time during which the sprite does not flicker (1% to 100%).")]
    [Range(1, 100)]
    public int flickerPercentage = 25;
    [Tooltip("Time between each flicker.")]
    [Range(0.033f, 0.2f)]
    public float flickerSpeed = 0.067f;
}
