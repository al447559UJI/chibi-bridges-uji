using UnityEngine;
using UnityEngine.Events;

public enum ActionStateType
{
    BUILD,
    INACTIVE,
    AIR_MELEE,
    MELEE,
    SHOOT
}

public class ActionStateManager : MonoBehaviour
{
    private ActionBaseState currentState;
    public PlayerController controller;

    public ActionBuildState buildState = new ActionBuildState();
    public ActionInactiveState inactiveState = new ActionInactiveState();
    public ActionAirMeleeState airMeleeState = new ActionAirMeleeState();
    public ActionMeleeState meleeState = new ActionMeleeState();
    public ActionShootState shootState = new ActionShootState();

    public UnityEvent<ActionStateType> onStateEntered;
    public UnityEvent<ActionStateType> onStateExited;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
        DebugRegistry.Register("Current Action", () => GetCurrentStateName());
    }

    void Start()
    {
        currentState = inactiveState;
        currentState.EnterState(this);
    }


    void Update()
    {
        currentState.UpdateState(this);
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        if (currentState == state) return;

        currentState.ExitState(this);
        onStateExited.Invoke(currentState.Type);

        currentState = state;

        state.EnterState(this);
        onStateEntered.Invoke(state.Type);
    }
    

    public string GetCurrentStateName()
    {
        if (currentState != null)
        {
            return currentState.GetType().Name;
        }
        return "NoState";
    }
}
