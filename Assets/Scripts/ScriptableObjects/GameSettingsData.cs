using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsData", menuName = "Scriptable Objects/GameSettingsData")]
public class GameSettingsData : ScriptableObject
{
    public UIScale uiScale = UIScale.SMALL;
    public float debugTimeScale = 1f;
}
