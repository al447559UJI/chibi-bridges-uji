using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealthData", menuName = "Scriptable Objects/PlayerHealthData")]
public class PlayerHealthData : ScriptableObject
{
    [Header("Debug toggles")]
    public bool debugGodMode = false;

    [Header("Health parameters")]
    public int maxHealth = 100;
    public float invencibleTime = .5f;
}
