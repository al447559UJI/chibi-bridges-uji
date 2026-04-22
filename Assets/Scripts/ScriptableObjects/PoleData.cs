using UnityEngine;

[CreateAssetMenu(fileName = "PoleData", menuName = "Scriptable Objects/PoleData")]
public class PoleData : ScriptableObject
{
    [Range(50f, 400f)]
    public float motorSpeed;
    [Range(100f, 400f)]
    public float maxMotorTorque;
    [Range(0.5f, 4f)]
    public float gravityScale;
    public int destroyScrapAmount;
}
