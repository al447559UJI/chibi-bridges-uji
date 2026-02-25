using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerInput input { get; private set; }
    public PlayerMovement movement { get; private set; }
    public PlayerActions actions { get; private set; }

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        actions = GetComponent<PlayerActions>();
    }

}