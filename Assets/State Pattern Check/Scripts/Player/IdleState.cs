using UnityEngine;

public class IdleState : BaseState<PlayerState>
{
    public IdleState() : base(PlayerState.Idle) { }

    public override void Initialize(StateManager<PlayerState> manager)
    {
        // Initialize state-specific data here if needed.
    }

    public override void EnterState()
    {
        // Add logic for entering the Idle state.
        Debug.Log("Entering Idle State");
    }

    public override void UpdateState()
    {
        // Add logic for the Idle state.
        Debug.Log("Idle State Update");
    }

    public override void ExitState()
    {
        // Add logic for exiting the Idle state.
        Debug.Log("Exiting Idle State");
    }

    public override PlayerState GetNextState()
    {
        // Implement the condition to transition to the next state.
        if (Input.GetKey(KeyCode.W))
        {
            return PlayerState.Walking;
        }
        return StateKey;
    }

    public override void OnTriggerEnter(Collider other)
    {
        // Handle collisions for the Idle state.
    }

    public override void OnTriggerStay(Collider other)
    {
        // Handle collisions for the Idle state.
    }

    public override void OnTriggerExit(Collider other)
    {
        // Handle collisions for the Idle state.
    }

    public override void HandleEvent(object eventData)
    {
        // Handle custom events for state transitions.
    }

    public override bool ShouldTransition()
    {
        // Implement custom conditions for state transitions.
        return true;
    }
}
