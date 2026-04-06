using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerInput input { get; private set; }
    public PlayerMovement movement { get; private set; }
    public PlayerActions actions { get; private set; }
    public PlayerHealth health { get; private set; }

    private MovementStateManager movementStateManager;
    // private ActionStateManager actionStateManager;


    void OnEnable()
    {
        health.onDeath.AddListener(HandleDeath);

    }
    void OnDisable()
    {
        health.onDeath.RemoveListener(HandleDeath);
    }
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        actions = GetComponent<PlayerActions>();
        health = GetComponent<PlayerHealth>();

        movementStateManager = GetComponent<MovementStateManager>();
        // actionStateManager = GetComponent<ActionStateManager>();
    }

    private void HandleDeath()
    {
        movementStateManager.SwitchState(movementStateManager.deathState);
    }

}