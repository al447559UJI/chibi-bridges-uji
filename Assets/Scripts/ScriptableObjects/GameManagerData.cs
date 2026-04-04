using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "Scriptable Objects/GameManagerData")]
public class GameManagerData : ScriptableObject
{
    [Header("Debug toggles")]
    [Range(0.05f, 2f)]
    public float debugTimeScale = 1;
}
