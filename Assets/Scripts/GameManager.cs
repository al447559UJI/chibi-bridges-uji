using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerData data;

    void Start()
    {
        // OnValidate() doesn't get called when changing values on ScriptableObjects.
        Time.timeScale = data.debugTimeScale;
    }

}
